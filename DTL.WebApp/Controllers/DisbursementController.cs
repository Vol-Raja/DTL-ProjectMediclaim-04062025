using DTL.Business.Common;
using DTL.Business.Disbursement;
using DTL.Business.PensionSlip;
using DTL.Model.CommonModels;
using DTL.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTL.Business.EmployeeRegistration;
using System.IO;
using DTL.Business.Dapper;
using DTL.WebApp.Common.Extensions;
using DTL.Model.Models.UserManagement;
using DTL.Business.UserManagement;

namespace DTL.WebApp.Controllers
{
    [Authorize]
    public class DisbursementController : Controller
    {
        private static IAddTDSCalculation _TDSCalculationService;
        private static IAddEmployee _addEmployeeService;
        private static IPensionSlipService _pensionSlipService;
        private static IAddQtmPayment _addQtmPaymentService;
        private static IRecoveryAllowance _recoveryAllowanceService;
        private static IDisbursementReport _disbursementReportService;
        private static IAddTDSInvestment _TDSInvestmentService;
        private static IEnumerable<AddQtmPaymentModel> _addQtmPaymentModels;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;


        public DisbursementController(IAddQtmPayment addQtmPaymentService,
            IAddTDSCalculation TDSCalculationService,
            IAddEmployee addEmployeeService,
            IPensionSlipService pensionSlipService,
            IRecoveryAllowance recoveryAllowanceService,
            IDisbursementReport disbursementReport,
            IAddTDSInvestment TDSInvestmentService,
            IAssignPermission assignPermission)
        {
            _addQtmPaymentService = addQtmPaymentService;
            _TDSCalculationService = TDSCalculationService;
            _addEmployeeService = addEmployeeService;
            _pensionSlipService = pensionSlipService;
            _recoveryAllowanceService = recoveryAllowanceService;
            _disbursementReportService = disbursementReport;
            _TDSInvestmentService = TDSInvestmentService;
            _assignPermission = assignPermission;
        }

        public IActionResult Index()
        {
            IEnumerable<AssignPermissionModel> assignPermissionModels = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, null, null);

            return View(assignPermissionModels);
        }

        public IActionResult TDSCalculation()
        {
            DateTime dt = DateTime.Now;

            int CurrentYear = 0;
            int PrevYear = 0;
            int NextYear = 0;
            int CurrMonth = 0;
            CurrentYear = dt.Year;
            CurrMonth = dt.Month;
            PrevYear = dt.Year - 1;
            NextYear = dt.Year + 1;
            List<int> yrs = new List<int>();
            yrs.Add(PrevYear);
            yrs.Add(CurrentYear);
            yrs.Add(NextYear);
            ViewBag.FinancialYear = yrs;
            ViewBag.Pensioner = new List<string>()
            {
                "DPCL - Delhi Power Company Limited",
                "IPGCL - Indraprastha Power Generation Company Limited",
                "DTL - Delhi Transco Limited",
                "BRPL - BSES Rajdhani Power Limited",
                "BYPL - BSES Yamuna Power Limited",
                "TPDDL - Tata Power Delhi Distribution Limited",
            };

            GetPermissions("Disbursement", "TDS Calculation");
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;

            return View("TDSCalculation", new TDSCalculationModel());
        }


        [HttpPost]
        public JsonResult AddEditTDSCalculation(TDSCalculationModel tDSCalculation, TDSInvestmentModel tDSInvestment)
        {
            try
            {

                foreach (var file in Request.Form.Files)
                {

                    using var fs1 = file.OpenReadStream();
                    using var ms1 = new MemoryStream();
                    fs1.CopyTo(ms1);
                    switch (file.Name)
                    {
                        case "file80D":
                            tDSInvestment.InvFile80D = ms1.ToArray();
                            break;
                        case "file80DD":
                            tDSInvestment.InvFile80DD = ms1.ToArray();
                            break;
                        case "file80DDB":
                            tDSInvestment.InvFile80DDB = ms1.ToArray();
                            break;
                        case "file5YrsTermDpositPostOffice":
                            tDSInvestment.InvFile5YrsTermDepositPostoffice = ms1.ToArray();
                            break;
                        case "fileLIC_Pension_Plan":
                            tDSInvestment.InvFileLIC_Pension_Plan = ms1.ToArray();
                            break;
                        case "fileNSC":
                            tDSInvestment.InvFileNSC = ms1.ToArray();
                            break;
                        case "filePPF":
                            tDSInvestment.InvFilePPF = ms1.ToArray();
                            break;
                        case "fileStampDuty":
                            tDSInvestment.InvFileStampDuty = ms1.ToArray();
                            break;
                        case "fileSukanyaSmriddhiYojana":
                            tDSInvestment.InvFileSukanyaSmriddhiYojana = ms1.ToArray();
                            break;
                        case "fileTermDepositBank":
                            tDSInvestment.InvFileTermDepositBank = ms1.ToArray();
                            break;
                        case "fileTF":
                            tDSInvestment.InvFileTF = ms1.ToArray();
                            break;
                        case "fileULIP":
                            tDSInvestment.InvFileULIP = ms1.ToArray();
                            break;
                        case "fileMF":
                            tDSInvestment.InvFileMF = ms1.ToArray();
                            break;
                        case "fileHousingLoanInt":
                            tDSInvestment.InvFileHousingLoanInt = ms1.ToArray();
                            break;
                        case "fileHousingLoanInt1617":
                            tDSInvestment.InvFileHousingLoanInt1617 = ms1.ToArray();
                            break;
                        case "fileHousingLoanInt1920":
                            tDSInvestment.InvFileHousingLoanInt1920 = ms1.ToArray();
                            break;
                        case "file80E":
                            tDSInvestment.InvFile80E = ms1.ToArray();
                            break;
                        case "file80G":
                            tDSInvestment.InvFile80G = ms1.ToArray();
                            break;
                        case "file80GGB":
                            tDSInvestment.InvFile80GGB = ms1.ToArray();
                            break;
                        case "file80GGC":
                            tDSInvestment.InvFile80GGC = ms1.ToArray();
                            break;
                        case "file80GG":
                            tDSInvestment.InvFile80GG = ms1.ToArray();
                            break;
                        case "file80RRB":
                            tDSInvestment.InvFile80RRB = ms1.ToArray();
                            break;
                        case "file80TTA":
                            tDSInvestment.InvFile80TTA = ms1.ToArray();
                            break;
                        case "file80TTB":
                            tDSInvestment.InvFile80TTB = ms1.ToArray();
                            break;
                        case "file80U":
                            tDSInvestment.InvFile80U = ms1.ToArray();
                            break;
                    }

                }
                Guid result;
                var res = "";
                if (tDSCalculation.ID == Guid.Empty)
                {
                    tDSCalculation.CreatedBy = User.Identity.Name;
                    tDSCalculation.ModifideBy = "";
                    result = _TDSCalculationService.AddEditTDSCalculation(tDSCalculation, false, tDSInvestment);
                    if (result != Guid.Empty)
                    {
                        tDSInvestment.CreatedBy = User.Identity.Name;
                        tDSInvestment.ModifideBy = "";
                        tDSInvestment.TDSCalculationId = result;
                        res = _TDSInvestmentService.AddEditTDSInvestment(tDSInvestment, false);

                    }
                }
                else
                {
                    tDSCalculation.ModifideBy = User.Identity.Name;

                    result = _TDSCalculationService.AddEditTDSCalculation(tDSCalculation, true, tDSInvestment);
                    tDSInvestment.ModifideBy = User.Identity.Name;
                    res = _TDSInvestmentService.AddEditTDSInvestment(tDSInvestment, true);

                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }

        public IActionResult GetFinancialYear()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetEmployee(string DtlOffice, string SearchType, string SearchVal)
        {

            //IEnumerable<EmployeeProfileModel> EmpData;// = new IEnumerable<EmployeeProfileModel>();
            if (DtlOffice == null)
                DtlOffice = "";
            var empData = _addEmployeeService.GetEmployeeByDTLOffice(DtlOffice, SearchType, SearchVal);

            //var iEnm = EmpData as IEnumerable<EmployeeProfileModel>;
            //List<EmployeeProfileModel> lst = iEnm.ToList();

            if (SearchType == "EmployeeId")
            {
                var res = (from N in empData.ToList()
                           where N.EmployeeId.ToString().StartsWith(SearchVal) 
                           select new
                           {
                               empid = N.EmployeeId,
                               EmpName = N.EmployeeName,
                               RegId = N.ID,
                               dob = N.DOB,
                               age = N.age,
                               tdsId = N.TDSId
                           }).ToList();
                return Json(res);
            }
            if (SearchType == "EmployeeName")
            {
                var res = (from N in empData.ToList()
                           where N.EmployeeName.ToLower().StartsWith(SearchVal.ToLower())
                           select new
                           {
                               label = N.EmployeeName,
                               val = N.EmployeeId,
                               RegId = N.ID,
                               dob = N.DOB,
                               age = N.age,
                               tdsId = N.TDSId
                           }).ToList();
                return Json(res);
            }

            return Json("");



        }


        public IActionResult AgeQuantamPay()
        {
            //PopulateStaticData();
            var populateList = ReadJson.LoadJson();
            //AddQtmPaymentModel addQtmPaymentModel = new AddQtmPaymentModel();
            ViewBag.DTLOfficeList = populateList.DTLOfficeList;
            ViewBag.AgeGroupList = populateList.AgeGroupList;

            GetPermissions("Disbursement", "Age Quantum Pay");
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;

            return View(new AddQtmPaymentModel());
        }

        [HttpPost]
        public async Task<JsonResult> AutoComplete(string prefix)
        {
            _addQtmPaymentModels = await _addQtmPaymentService.GetPensionersAllAsync();
            var pensioner = (from data in _addQtmPaymentModels
                             where data.PensionerName.ToLower().StartsWith(prefix.ToLower())
                             select new
                             {
                                 label = data.PensionerName,
                                 name = data.PensionerName,
                                 val = data.EmployeeID,
                                 cr = data.CurrentAge,
                                 dob = data.DOB,
                                 employerName = data.EmployerName,
                                 monthlyPension = data.MonthlyPension,
                                 aqpercentage = data.IncrementPercentage,
                                 incremenetedAmount = data.IncrementAmount,
                                 monthlyAQP = data.AQPMonthlyPension,
                                 ageGroup = data.AgeGroup,
                                 fromDate = data.FromDate,
                                 toDate = data.ToDate,
                                 id = data.ID,
                                 eid = data.EmployeeRegistrationId
                             }).ToList();
            return Json(pensioner);
        }

        [HttpPost]
        public JsonResult SaveAQPayment(AddQtmPaymentModel addQtmPaymentModel)
        {
            bool isUpdate = !(addQtmPaymentModel.ID == Guid.Empty);
            if (!isUpdate)
                addQtmPaymentModel.CreatedBy = User.Identity.Name;
            else
                addQtmPaymentModel.ModifideBy = User.Identity.Name;
            var result = _addQtmPaymentService.InsertUpdateAddQtmPaymentDetails(addQtmPaymentModel, isUpdate);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetPensionData(Guid EmpRegId, int FinYear)
        {
            var penData = _pensionSlipService.GetPensionDataByFinanacialYear(EmpRegId, FinYear);

            return Json(penData);
        }

        [HttpPost]
        public JsonResult GetTDSCalculationDetails(Guid EmpRegId, int FinYear)
        {
            var TDSData = _TDSCalculationService.GetTDSCalculationDetails(EmpRegId, FinYear);

            return Json(TDSData);
        }
        [HttpPost]
        public JsonResult GetTDSInvestmentDetails(Guid EmpRegId, Guid TDSCalculationId)
        {
            var TDSInvData = _TDSInvestmentService.GetTDSInvestmentDetails(TDSCalculationId);
            if (TDSInvData != null)
            {
                if (TDSInvData.InvFile80D != null)
                    TDSInvData.InvFilePath80D = TDSInvData.InvFile80D.ConvertBytesToPDF("InvFile80D_" + EmpRegId);

                if (TDSInvData.InvFile80DD != null)
                    TDSInvData.InvFilePath80DD = TDSInvData.InvFile80DD.ConvertBytesToPDF("InvFile80DD_" + EmpRegId);

                if (TDSInvData.InvFile80DDB != null)
                    TDSInvData.InvFilePath80DDB = TDSInvData.InvFile80DDB.ConvertBytesToPDF("InvFile80DDB_" + EmpRegId);

                if (TDSInvData.InvFile5YrsTermDepositPostoffice != null)
                    TDSInvData.InvFilePath5YrsTermDepositPostoffice =
                        TDSInvData.InvFile5YrsTermDepositPostoffice.ConvertBytesToPDF("InvFile5YrsTermDepositPostoffice" + EmpRegId);

                if (TDSInvData.InvFile80E != null)
                    TDSInvData.InvFilePath80E = TDSInvData.InvFile80E.ConvertBytesToPDF("InvFile80E_" + EmpRegId);

                if (TDSInvData.InvFile80G != null)
                    TDSInvData.InvFilePath80G = TDSInvData.InvFile80G.ConvertBytesToPDF("InvFile80G_" + EmpRegId);

                if (TDSInvData.InvFile80GGB != null)
                    TDSInvData.InvFilePath80GGB = TDSInvData.InvFile80GGB.ConvertBytesToPDF("InvFile80GGB_" + EmpRegId);

                if (TDSInvData.InvFile80GGC != null)
                    TDSInvData.InvFilePath80GGC = TDSInvData.InvFile80GGC.ConvertBytesToPDF("InvFile80GGC_" + EmpRegId);

                if (TDSInvData.InvFileHousingLoanInt != null)
                    TDSInvData.InvFilePathHousingLoanInt =
                        TDSInvData.InvFileHousingLoanInt.ConvertBytesToPDF("InvFileHousingLoanInt_" + EmpRegId);

                if (TDSInvData.InvFileHousingLoanInt1617 != null)
                    TDSInvData.InvFilePathHousingLoanInt1617 =
                        TDSInvData.InvFileHousingLoanInt1617.ConvertBytesToPDF("InvFileHousingLoanInt1617_" + EmpRegId);

                if (TDSInvData.InvFileHousingLoanInt1920 != null)
                    TDSInvData.InvFilePathHousingLoanInt1920 =
                        TDSInvData.InvFileHousingLoanInt1920.ConvertBytesToPDF("InvFileHousingLoanInt1920_" + EmpRegId);

                if (TDSInvData.InvFileLIC_Pension_Plan != null)
                    TDSInvData.InvFilePathLIC_Pension_Plan =
                        TDSInvData.InvFileLIC_Pension_Plan.ConvertBytesToPDF("InvFileLIC_Pension_Plan_" + EmpRegId);

                if (TDSInvData.InvFileMF != null)
                    TDSInvData.InvFilePathMF = TDSInvData.InvFileMF.ConvertBytesToPDF("InvFileMF_" + EmpRegId);

                if (TDSInvData.InvFileNSC != null)
                    TDSInvData.InvFilePathNSC = TDSInvData.InvFileNSC.ConvertBytesToPDF("InvFileNSC_" + EmpRegId);

                if (TDSInvData.InvFile5YrsTermDepositPostoffice != null)
                    TDSInvData.InvFilePath5YrsTermDepositPostoffice =
                        TDSInvData.InvFile5YrsTermDepositPostoffice.ConvertBytesToPDF("InvFilePath5YrsTermDepositPostoffice_" + EmpRegId);

                if (TDSInvData.InvFilePPF != null)
                    TDSInvData.InvFilePathPPF = TDSInvData.InvFilePPF.ConvertBytesToPDF("InvFilePPF_" + EmpRegId);

                if (TDSInvData.InvFileStampDuty != null)
                    TDSInvData.InvFilePathStampDuty = TDSInvData.InvFileStampDuty.ConvertBytesToPDF("InvFileStampDuty_" + EmpRegId);

                if (TDSInvData.InvFileSukanyaSmriddhiYojana != null)
                    TDSInvData.InvFilePathSukanyaSmriddhiYojana = TDSInvData.InvFileSukanyaSmriddhiYojana.ConvertBytesToPDF("InvFileSukanyaSmriddhiYojana_" + EmpRegId);

                if (TDSInvData.InvFileTermDepositBank != null)
                    TDSInvData.InvFilePathTermDepositBank =
                        TDSInvData.InvFileTermDepositBank.ConvertBytesToPDF("InvFileTermDepositBank_" + EmpRegId);

                if (TDSInvData.InvFileTF != null)
                    TDSInvData.InvFilePathTF = TDSInvData.InvFileTF.ConvertBytesToPDF("InvFileTF_" + EmpRegId);

                if (TDSInvData.InvFileULIP != null)
                    TDSInvData.InvFilePathULIP = TDSInvData.InvFileULIP.ConvertBytesToPDF("InvFileULIP_" + EmpRegId);

                if (TDSInvData.InvFile80GG != null)
                    TDSInvData.InvFilePath80GG = TDSInvData.InvFile80GG.ConvertBytesToPDF("InvFile80GG_" + EmpRegId);

                if (TDSInvData.InvFile80RRB != null)
                    TDSInvData.InvFilePath80RRB = TDSInvData.InvFile80RRB.ConvertBytesToPDF("InvFile80RRB_" + EmpRegId);
                if (TDSInvData.InvFile80TTA != null)
                    TDSInvData.InvFilePath80TTA = TDSInvData.InvFile80TTA.ConvertBytesToPDF("InvFile80TTA_" + EmpRegId);
                if (TDSInvData.InvFile80TTB != null)
                    TDSInvData.InvFilePath80TTB = TDSInvData.InvFile80TTB.ConvertBytesToPDF("InvFile80TTB_" + EmpRegId);
                if (TDSInvData.InvFile80U != null)
                    TDSInvData.InvFilePath80U = TDSInvData.InvFile80U.ConvertBytesToPDF("InvFile80U_" + EmpRegId);


            }
            return Json(TDSInvData);
        }



        public IActionResult RecoveryAllowance()
        {
            CommonModel populateList = ReadJson.LoadJson();
            RecoveryAllowanceModel recoveryAllowanceModel = new RecoveryAllowanceModel();
            recoveryAllowanceModel.DTLOfficeList = populateList.DTLOfficeList;
            recoveryAllowanceModel.ChangeTypeList = populateList.ChangeTypeList;
            recoveryAllowanceModel.ReasonList = populateList.ReasonList;
            recoveryAllowanceModel.RecoveryOptionList = populateList.RecoveryOptionList;
            recoveryAllowanceModel.TypeOfRecoveryList = populateList.TypeOfRecoveryList;

            GetPermissions("Disbursement", "Recovery/Allowance");
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;

            return View(recoveryAllowanceModel);
        }

        [HttpPost]
        public async Task<JsonResult> FetchPensioner(string prefix)
        {

            var _recoveryAllowanceModels = await _recoveryAllowanceService.GetPensionersAllAsync();
            var pensioner = (from data in _recoveryAllowanceModels
                             where data.PensionerName.ToLower().StartsWith(prefix.ToLower())
                             select new
                             {
                                 label = data.PensionerName,
                                 name = data.PensionerName,
                                 val = data.EmployeeID,
                                 employerName = data.EmployerName,
                                 typeOfRecovery = data.TypeOfRecovery,
                                 recoveryAmount = data.RecoveryAmount,
                                 recoveryOption = data.RecoveryOption,
                                 monthlyPension = data.MonthlyPension,
                                 applicable = data.ApplicableAmount,
                                 updatedPension = data.MonthlyPensionAfter,
                                 froms = data.FromDate,
                                 to = data.ToDate,
                                 changeType = data.ChangeType,
                                 reason = data.Reason,
                                 id = data.ID,
                                 eid = data.EmployeeRegistrationId
                             }).ToList();
            return Json(pensioner);
        }

        [HttpPost]
        public JsonResult SaveRecoveryAllowanceData(RecoveryAllowanceModel recoveryAllowanceModel)
        {
            bool isUpdate = !(recoveryAllowanceModel.ID == Guid.Empty);
            if (!isUpdate)
                recoveryAllowanceModel.CreatedBy = User.Identity.Name;
            else
                recoveryAllowanceModel.ModifideBy = User.Identity.Name;

            var result = _recoveryAllowanceService.InsertUpdateRecoveryAllowanceDetails(recoveryAllowanceModel, isUpdate);

            return Json(result);
        }

        public IActionResult DisbursementReport(string year = null, string month = null, string DTLOffice = null, string EmployeeID = null)
        {
            CommonModel populateList = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = populateList.DTLOfficeList;
            DateTime dt = DateTime.Today;
            var _reports = _disbursementReportService.GetDisbursementReportData(dt, DTLOffice, EmployeeID);
            return View(_reports);
        }

        public JsonResult GetDisbursementReport(string year = null, string month = null, string DTLOffice = null, string EmployeeID = null)
        {
            DateTime dt = Convert.ToDateTime((string.Format("{0}-{1}-01", year, month)));
            var _reports = _disbursementReportService.GetDisbursementReportData(dt, DTLOffice, EmployeeID);
            return Json(_reports);
        }

        private void GetPermissions(string modulename, string submodulename)
        {
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, modulename, submodulename).FirstOrDefault();
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