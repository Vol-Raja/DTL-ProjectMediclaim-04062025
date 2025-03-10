using ClosedXML.Excel;
using DTL.Business.Common;
using DTL.Business.GPF.Processing;
using DTL.Model.Models.GPF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
//using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Text;
using iText.Layout;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using DTL.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;

namespace DTL.WebApp.Areas.GPF.Controllers
{
    [Area("GPF")]
    [Authorize(Roles = "DVB Pension Trust,Admin")]
    public class ProcessingController : Controller
    {
        private readonly IProcessing _processing;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;

        public ProcessingController(IProcessing processing, IAssignPermission assignPermission)
        {
            _processing = processing;
            _assignPermission = assignPermission;
        }
        public IActionResult Dashboard() {
            return View();
        }   
        public IActionResult Summary() {
            return View();
        }
        public IActionResult MonthlyProcessing() {
            return View();
        }
        public IActionResult CurrentGPFBalance()
        {
            return View();
        }
        public IActionResult YearlyStatements(string fy, string accountNumber, string orgCode)
        {
            if (string.IsNullOrWhiteSpace(fy) || string.IsNullOrWhiteSpace(accountNumber) || string.IsNullOrWhiteSpace(orgCode))
                return RedirectToAction("StatementForm");
            ViewBag.FY = fy;
            ViewBag.AccountNumber = accountNumber;
            ViewBag.OrgCode = orgCode;
            return View();
        }
        public IActionResult StatementForm()
        {
            return View();
        }
        public IActionResult Index()
        {
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList;
            var _gpfProcessingSummary = _processing.GetGPFProcessingSummaryByParam(null, null, null);
            GetPermisisions();
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;
            return View(_gpfProcessingSummary);
        }

        /// <summary>
        /// uploading and reading excel file 
        /// to list of gpfProcessingModel
        /// </summary>
        /// <param name="file"></param>
        /// <returns>List of GPFProcessingModel</returns>
        [HttpPost]
        public IActionResult LoadFile(IFormFile file)
        {
            IEnumerable<GPFProcessing> gpfProcessingList = null;

            if (file.Length > 0)
            {
                gpfProcessingList = ReadExcelData(file);
            }
            return Json(gpfProcessingList);
        }

        /// <summary>
        /// Get GPF Processing details List
        /// </summary>
        /// <returns>List</returns>
        [Route("GPF/Processing/GetGPFProcessingDetail")]
        public IActionResult GetGPFProcessingList()
        {
            var gpfProcessingModel = _processing.GetDetailByParam(null, null, null, null,null);
            return Json(gpfProcessingModel);
        }

        /// <summary>
        /// Used to search gpf details by parameters
        /// </summary>
        /// <param name="processingDate"></param>
        /// <param name="employer"></param>
        /// <param name="employeeId"></param>
        /// <returns>Processing Detail List</returns>
        [Route("GPF/Processing/GetGPFProcessingDetail/{processingDate?}/{employer?}/{employeeId?}")]
        public IActionResult GetGPFProcessingDetail([FromRoute] string processingDate, [FromRoute] string employer = null, [FromRoute] string employeeId = null) 
        {
            //1%2F1%2F0001

            employer = employer!="NA" ? employer : null;
            var month = !string.IsNullOrEmpty(processingDate) ? (int.TryParse(processingDate.Split('-')[0].Trim(), out var i) ? (int?)i : null) : null;
            var year = !string.IsNullOrEmpty(processingDate) ? (int.TryParse(processingDate.Split('-')[1].Trim(), out var j) ? (int?)j : null) : null;
            employeeId = employeeId!="0"? employeeId : null;

            var gpfProcessingModel = _processing.GetDetailByParam(employer,month,year,employeeId,null);
            return Json(gpfProcessingModel);
        }

        /// <summary>
        /// Get Processing Detail Summary
        /// </summary>
        /// <returns>Processing Detail List</returns>
        [HttpGet]
        [Route("GPF/Processing/GetGPFProcessingDetail/Summary")]
        [Route("GPF/Processing/GetGPFProcessingDetail/Summary/{employer}")]
        public IActionResult GetGPFProcessingDetailSummary([FromRoute] string employer = null)
        {
            var _gpfProcessingSummary = _processing.GetGPFProcessingSummaryByParam(employer, null, null);
            return Json(_gpfProcessingSummary);
        }

        /// <summary>
        /// Saving gpf details in db
        /// </summary>
        /// <param name="gpfProcessingModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveGPFProcessing([FromBody] GPFProcessingModel gpfProcessingModel)
        {
            //int _mediclaimId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    if (gpfProcessingModel.EmployeeType.ToLower() == "deputated")
                    {
                        var _calculatedInterestList = from x in gpfProcessingModel.GPFProcessingList
                                                      select new GPFProcessing
                                                      {
                                                          EmployeeType = gpfProcessingModel.EmployeeType,
                                                          EmployeeNumber = x.EmployeeNumber,
                                                          EmployeeName = x.EmployeeName,
                                                          Employer = x.Employer,
                                                          Designation = x.Designation,
                                                          MemberContribution = x.MemberContribution,
                                                          MemberInterest = x.GPFAmount * (decimal.Parse(gpfProcessingModel.Interest)/100),
                                                          GPFAmount = x.GPFAmount,
                                                          Month = x.Month,
                                                          Year = x.Year
                                                      };

                        _processing.SaveGPFProcessing(_calculatedInterestList.ToList(), User.Identity.Name);
                    }

                    if (gpfProcessingModel.EmployeeType.ToLower() == "general")
                    {
                        //Check processing date if it is before 4th of current Month  
                        if (gpfProcessingModel.ProcessingDate.Day > 4)
                        {
                            var _currentDate = DateTime.Now;
                            var _previousMonthDate = _currentDate.AddMonths(-1);
                            var _previousMonthList = _processing.GetDetailByParam(null, _previousMonthDate.Month, _previousMonthDate.Year, null,null);

                            var _calculatedInterestList = from x in gpfProcessingModel.GPFProcessingList
                                                          join y in _previousMonthList
                                                          on x.EmployeeNumber equals y.EmployeeNumber into temp
                                                          from t in temp.DefaultIfEmpty()
                                                          select new GPFProcessing
                                                          {
                                                              EmployeeType = gpfProcessingModel.EmployeeType,
                                                              EmployeeNumber = x.EmployeeNumber,
                                                              EmployeeName = x.EmployeeName,
                                                              Employer = x.Employer,
                                                              Designation = x.Designation,
                                                              MemberContribution = x.MemberContribution,
                                                              MemberInterest = t.GPFAmount * (decimal.Parse(gpfProcessingModel.Interest)/100),
                                                              GPFAmount = x.GPFAmount,
                                                              Month = x.Month,
                                                              Year = x.Year
                                                          };

                            _processing.SaveGPFProcessing(_calculatedInterestList.ToList(), User.Identity.Name);
                        }
                        else
                        {
                            var _calculatedInterestList = from x in gpfProcessingModel.GPFProcessingList
                                                          select new GPFProcessing
                                                          {

                                                              EmployeeType = gpfProcessingModel.EmployeeType,
                                                              EmployeeNumber = x.EmployeeNumber,
                                                              EmployeeName = x.EmployeeName,
                                                              Employer = x.Employer,
                                                              Designation = x.Designation,
                                                              MemberContribution = x.MemberContribution,
                                                              MemberInterest = x.GPFAmount * (decimal.Parse(gpfProcessingModel.Interest)/100),
                                                              GPFAmount = x.GPFAmount,
                                                              Month = x.Month,
                                                              Year = x.Year
                                                          };

                            _processing.SaveGPFProcessing(_calculatedInterestList.ToList(), User.Identity.Name);
                        }
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok();
        }

        public FileResult ExportGPFReportToExcel()
        {
            IEnumerable<GPFProcessing> _gpfProcessingList;
            DataTable dt = new DataTable("GPF Report List");

            _gpfProcessingList = _processing.GetDetailByParam(null, null, null, null,null);
            //dt.Load((IDataReader)_gpfProcessingList);


            dt.Columns.Add("Employee Number", typeof(string));
            dt.Columns.Add("Employee Name", typeof(string));
            dt.Columns.Add("Employer ", typeof(string));
            dt.Columns.Add("Designation", typeof(string));
            dt.Columns.Add("Member Contribution", typeof(decimal));
            dt.Columns.Add("Member Interest ", typeof(decimal));
            dt.Columns.Add("GPFAmount", typeof(decimal));
            dt.Columns.Add("Month", typeof(string));
            dt.Columns.Add("Year", typeof(int));

            foreach (var gpfprocessing in _gpfProcessingList)
            {
                dt.Rows.Add(gpfprocessing.EmployeeNumber,
                    gpfprocessing.EmployeeName,
                    gpfprocessing.Employer,
                    gpfprocessing.Designation,
                    gpfprocessing.MemberContribution,
                    gpfprocessing.MemberInterest,
                    gpfprocessing.GPFAmount,
                    MonthName(gpfprocessing.Month),
                    gpfprocessing.Year);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GPFReport.xlsx");
                }
            }
        }

        public FileResult ExportGPFSummaryToExcel()
        {
            IEnumerable<GPFProcessing> _gpfProcessingList;
            DataTable dt = new DataTable("GPF Summary List");

            _gpfProcessingList = _processing.GetGPFProcessingSummaryByParam(null, null, null);
            //dt.Load((IDataReader)_gpfProcessingList);

            dt.Columns.Add("Month-Year", typeof(string));
            dt.Columns.Add("Member Contribution", typeof(decimal));
            dt.Columns.Add("Member Interest ", typeof(decimal));

            foreach (var gpfprocessing in _gpfProcessingList)
            {
                dt.Rows.Add(MonthName(gpfprocessing.Month)+"-"+ gpfprocessing.Year,
                    gpfprocessing.MemberContribution,
                    gpfprocessing.MemberInterest);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GPFSummary.xlsx");
                }
            }
        }

        [HttpGet]
        [Route("GPF/Processing/ExportGPFSummaryToPDF/{employer?}")]
        public IActionResult ExportGPFSummaryToPDF([FromRoute] string employer = null)
        {
            IEnumerable<GPFProcessing> _gpfProcessingList;

            _gpfProcessingList = _processing.GetGPFProcessingSummaryByParam(null, null, null);

            var name = "GPFSummary" + DateTime.Now.ToString("MMddyyyy") + ".pdf";

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"files/temp", name);
            var returnfilepath = Path.Combine($"files/temp", name);

            Table tbl = new Table(3).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
            Paragraph header = new Paragraph("GPF Summary Report").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

            using (PdfWriter pdfWriter = new PdfWriter(filepath))
            using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
            using (Document document = new Document(pdfDocument))
            {
                document.Add(header);

                Cell c1 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph("Month-Year")).SetFontSize(10);
                Cell c2 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph("Member Contribution")).SetFontSize(10);
                Cell c3 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph("Member Interest")).SetFontSize(10);
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                //document.Add(new Paragraph("Hello World!"));
                foreach (var gpfprocessing in _gpfProcessingList)
                {
                    Cell c11 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph(MonthName(gpfprocessing.Month) + "-" + gpfprocessing.Year)).SetFontSize(9);
                    Cell c22 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(gpfprocessing.MemberContribution))).SetFontSize(9);
                    Cell c33 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(gpfprocessing.MemberInterest))).SetFontSize(9);

                    tbl.AddCell(c11);
                    tbl.AddCell(c22);
                    tbl.AddCell(c33);
                }
                document.Add(tbl);


                return Json(returnfilepath);
            }
        }

        [HttpGet]
        [Route("GPF/Processing/ExportGPFReportToPDF/{processingDate?}/{employer?}/{employeeId?}")]
        public IActionResult ExportGPFReportToPDF([FromRoute] string processingDate, [FromRoute] string employer = null, [FromRoute] string employeeId = null)
        {
            IEnumerable<GPFProcessing> _gpfReportList;

            employer = employer != "NA" ? employer : null;
            var month = !string.IsNullOrEmpty(processingDate) ? (int.TryParse(processingDate.Split('-')[0].Trim(), out var i) ? (int?)i : null) : null;
            var year = !string.IsNullOrEmpty(processingDate) ? (int.TryParse(processingDate.Split('-')[1].Trim(), out var j) ? (int?)j : null) : null;
            employeeId = employeeId != "0" ? employeeId : null;

            _gpfReportList = _processing.GetDetailByParam(employer, month, year, employeeId,null);

            var name = "GPFReport" + DateTime.Now.ToString("MMddyyyy") + ".pdf";

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"files/temp", name);
            var returnfilepath = Path.Combine($"files/temp", name);

            Table tbl = new Table(3).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
            Paragraph header = new Paragraph("GPF Report").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

            using (PdfWriter pdfWriter = new PdfWriter(filepath))
            using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
            using (Document document = new Document(pdfDocument))
            {
                document.Add(header);

                Cell c1 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph("Employee Type")).SetFontSize(10);
                Cell c2 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph("Employee Name")).SetFontSize(10);
                Cell c3 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph("Employer")).SetFontSize(10);
                Cell c4 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph("Employee ID")).SetFontSize(10);
                Cell c5 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph("Member Contribution")).SetFontSize(10);
                Cell c6 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph("Member interest")).SetFontSize(10);
                Cell c7 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph("GPF Amount")).SetFontSize(10);
                Cell c8 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph("Month-Year")).SetFontSize(10);
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);
                tbl.AddCell(c6);
                tbl.AddCell(c7);
                tbl.AddCell(c8);
                //document.Add(new Paragraph("Hello World!"));
                foreach (var gpfreport in _gpfReportList)
                {
                    Cell c11 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph(MonthName(gpfreport.Month) + "-" + gpfreport.Year)).SetFontSize(9);
                    Cell c22 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(gpfreport.MemberContribution))).SetFontSize(9);
                    Cell c33 = new Cell(1, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(gpfreport.MemberInterest))).SetFontSize(9);

                    tbl.AddCell(c11);
                    tbl.AddCell(c22);
                    tbl.AddCell(c33);
                }
                document.Add(tbl);


                return Json(returnfilepath);
            }
        }

        private string MonthName(int numericMonth)
        {
            string _name = "";
            switch (numericMonth)
            {
                case 1: _name = "January"; break;
                case 2: _name = "February"; break;
                case 3: _name = "March"; break;
                case 4: _name = "April"; break;
                case 5: _name = "May"; break;
                case 6: _name = "June"; break;
                case 7: _name = "July"; break;
                case 8: _name = "August"; break;
                case 9: _name = "September"; break;
                case 10: _name = "October"; break;
                case 11: _name = "November"; break;
                case 12: _name = "December"; break;
                default: break;
            }
            return _name;
        }

        private IEnumerable<GPFProcessing> ReadExcelData(IFormFile file)
        {
            List<GPFProcessing> gpfProcessingList = new List<GPFProcessing>();

            var fileextension = Path.GetExtension(file.FileName);
            var filename = Guid.NewGuid().ToString() + fileextension;
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", filename);
            //var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");

            try
            {
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                }

                using (XLWorkbook workBook = new XLWorkbook(filepath))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Loop through the Worksheet rows.
                    bool firstRow = true;
                    int _row = 0;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        _row = _row + 1;
                        //Use the first row to add columns to DataTable.
                        //if (firstRow) { firstRow = false; }
                        if (_row <= 4) { continue;  }
                        else
                        {
                            var gpfProcessing = new GPFProcessing();

                            if (Convert.ToString(row.Cell(1).Value) != "")
                            {
                                //gpfProcessing.EmployeeNumber = row.Cell(1).Value.ToString();
                                //gpfProcessing.EmployeeName = row.Cell(2).Value.ToString();
                                //gpfProcessing.Employer = row.Cell(3).Value.ToString();
                                //gpfProcessing.Designation = row.Cell(4).Value.ToString();
                                //gpfProcessing.MemberContribution = Convert.ToDecimal(row.Cell(5).Value);
                                //gpfProcessing.MemberInterest = Convert.ToDecimal(row.Cell(6).Value);
                                //gpfProcessing.GPFAmount = Convert.ToDecimal(row.Cell(7).Value);
                                //gpfProcessing.Month = Convert.ToInt16(row.Cell(8).Value);
                                //gpfProcessing.Year = Convert.ToInt32(row.Cell(9).Value);

                                gpfProcessing.Employer = row.Cell(1).Value.ToString();
                                gpfProcessing.EmployeeName = "";
                                gpfProcessing.EmployeeNumber = row.Cell(2).Value.ToString();
                                gpfProcessing.Month = Convert.ToInt16(row.Cell(3).Value);
                                gpfProcessing.Year = Convert.ToInt32(row.Cell(4).Value);
                                gpfProcessing.Designation = "";
                                gpfProcessing.GPFAmount = Convert.ToDecimal(row.Cell(5).Value);
                                gpfProcessing.MemberContribution = Convert.ToDecimal(row.Cell(6).Value);
                                gpfProcessing.LoanAmount = Convert.ToDecimal(row.Cell(7).Value);
                                gpfProcessing.MemberInterest = Convert.ToDecimal(row.Cell(8).Value);
                                
                            }
                            else
                            {
                                break;
                            }
                            gpfProcessingList.Add(gpfProcessing);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
               System.IO.File.Delete(filepath);
                throw;
            }
            finally
            {
                System.IO.File.Delete(filepath);
            }

            return gpfProcessingList;
        }

        private void GetPermisisions()
        {
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "GPF", "LoanProcessing").FirstOrDefault();
            if (permission != null)
            {
                _create = permission.Create;
                _edit = permission.Edit;
                _view = permission.View;
                _delete = permission.Delete;
            }
        }
    }

}
