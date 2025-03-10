using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTL.Model.CommonModels;
using DTL.Model.Models;
using DTL.Business.Dapper;
using DTL.Business.Common;
using DTL.Business.FinanceManagement;
using System.Globalization;
using System.Text;
using DTL.Business.UserManagement;
using DTL.Model.Models.UserManagement;
using Microsoft.AspNetCore.Authorization;

namespace DTL.WebApp.Controllers
{

    [Authorize]
    public class FinanceManagementController : Controller
    {
        private static IAddBudgetDeclaration _BudgetDeclarationService;
        private static IPensionManagement _pensionManagement;
        private static IGPFManagement _gPFManagement;
        private static IAddInvestmentDeclaration _InvestmentDeclarationService;
        private readonly IAssignPermission _assignPermission;

        public FinanceManagementController(IAddBudgetDeclaration addBudgetDeclaration
            , IPensionManagement pensionManagement, IGPFManagement gPFManagement, 
            IAddInvestmentDeclaration addInvestmentDeclaration, IAssignPermission assignPermission)
        {
            _BudgetDeclarationService = addBudgetDeclaration;
            _pensionManagement = pensionManagement;
            _gPFManagement = gPFManagement;
            _InvestmentDeclarationService = addInvestmentDeclaration;
            _assignPermission = assignPermission;
        }

        public IActionResult Index()
        {
            IEnumerable<AssignPermissionModel> assignPermissionModels = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, null, null);

            return View(assignPermissionModels);
        }
        public IActionResult BudgetManagement()
        {
            DateTime dt = DateTime.Now;

            int CurrentYear = 0;
            int PrevYear = 0;
            int NextYear = 0;
            int CurrMonth = 0;
            int MostPrevYear = 0;
            CurrentYear = dt.Year;
            CurrMonth = dt.Month;
            PrevYear = dt.Year - 1;
            NextYear = dt.Year + 1;
            MostPrevYear = dt.Year - 2;
            List<String> yrs = new List<String>();
            yrs.Add(MostPrevYear.ToString() + "-" + PrevYear.ToString());
            yrs.Add(PrevYear.ToString() + "-" + CurrentYear.ToString());
            yrs.Add(CurrentYear.ToString() + "-" + NextYear.ToString());
            ViewBag.FinancialYear = yrs;

            var populateList = ReadJson.LoadJson();

            ViewBag.BudgetAllocationProgam = populateList.BudgetAllocationProgramList;
            ViewBag.DisbursementPeriod = populateList.DisbursementPeriodList;
            ViewBag.Designation = populateList.DesignationList;
            //return View("_BudgetDeclaration", new BudgetDeclarationModel());
            return View("BudgetManagement", new BudgetDeclarationModel());
        }
        [HttpPost]
        public JsonResult AddEditBudgetDeclaration(List<BudgetDeclarationModel> BudgetDeclaration)
        {
            string result = "";
            try
            {

                var res = "";
                foreach (var item in BudgetDeclaration)
                {
                    BudgetDeclarationModel bud = new BudgetDeclarationModel();
                    if (bud.ID == Guid.Empty)
                    {
                        if (item.Type == "Insert")
                        {
                            bud.CreatedBy = User.Identity.Name;
                            bud.ModifideBy = "";
                        }
                        else if (item.Type == "Update")
                        {
                            bud.CreatedBy = "";
                            bud.ModifideBy = User.Identity.Name;

                        }
                        bud.ID = item.ID;

                        bud.FinancialYear = item.FinancialYear;
                        bud.AllocationProgram = item.AllocationProgram;
                        bud.AllocatedFunds = item.AllocatedFunds;
                        bud.DisbursementPeriod = item.DisbursementPeriod;
                        bud.DisbursementAuthority = item.DisbursementAuthority;


                        result = _BudgetDeclarationService.AddEditBudgetDeclaration(bud, false);
                    }


                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }

        [HttpPost]
        public JsonResult GetBudgetDeclarationByFinancialYear(String FinancialYear)
        {
            var BudgetData = _BudgetDeclarationService.GetBudgetDeclarationByFinancialYear(FinancialYear);

            return Json(BudgetData);
        }
        public JsonResult DeleteBudgetByFinancialYear(String FinancialYear)
        {
            var res = _BudgetDeclarationService.DeleteBudgetByFinancialYear(FinancialYear);
            return Json(res);
        }
        public IActionResult PensionManagement()
        {
            CommonModel populateList = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = populateList.DTLOfficeList;
            return View();
        }

        public JsonResult GetOutflowReport(string month = null, string DTLOffice = null, string EmployeeID = null, string Flow = null)
        {
            if (month.Length == 6)
                month = string.Format("0{0}", month);
            DateTime dt = Convert.ToDateTime(DateTime.ParseExact((string.Format("01/{0}", month)), "dd/MM/yyyy", CultureInfo.InvariantCulture));
            var data = _pensionManagement.GetPensionManagementReport(dt, null, null, null);
            var _reports = _pensionManagement.GetPensionManagementReport(dt, DTLOffice, EmployeeID, null);
            var orders = _reports.Where(x => x.Employer != "")
                            .GroupBy(x => x.Employer)
                            .Select(g => new
                            {
                                Employer = g.Key,
                                Contribution = g.Sum(x => x.Contribution),
                                PensionAmount = g.Sum(x => x.PensionAmount)
                            });

            var Employers = orders.Select(x => x.Employer).ToList();
            List<decimal> temp2 = null;
            List<decimal> Contributions = null;
            List<decimal> PensionAmounts = null;
            if (Flow == "custom-tabs-three-pensionoutflow-tab")
            {
                temp2 = orders.Select(x => x.PensionAmount).ToList();
            }
            else if (Flow == "custom-tabs-three-pensioninflow-tab")
            {
                temp2 = orders.Select(x => x.Contribution).ToList();
            }
            else
            {
                Contributions = orders.Select(x => x.Contribution).ToList();
                PensionAmounts = orders.Select(x => x.PensionAmount).ToList();
            }

            return Json(new { _reports, Employers, temp2, Contributions, PensionAmounts });
        }

        public IActionResult GPFManagement()
        {
            CommonModel populateList = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = populateList.DTLOfficeList;
            return View();
        }

        public JsonResult GetGPFManagementReport(string month = null, string DTLOffice = null, string EmployeeID = null, string Flow = null)
        {
            if (month.Length == 6)
                month = string.Format("0{0}", month);
            DateTime dt = Convert.ToDateTime(DateTime.ParseExact((string.Format("01/{0}", month)), "dd/MM/yyyy", CultureInfo.InvariantCulture));
            var data = _gPFManagement.GetGPFManagementReport(dt, null, null, null);
            var _reports = _gPFManagement.GetGPFManagementReport(dt, DTLOffice, EmployeeID, null);
            var orders = _reports.Where(x => x.Employer != "")
                            .GroupBy(x => x.Employer)
                            .Select(g => new
                            {
                                Employer = g.Key,
                                GPFContribution = g.Sum(x => x.GPFContribution),
                                GPFWithdrawal = g.Sum(x => x.GPFWithdrawal)
                            });

            var Employers = orders.Select(x => x.Employer).ToList();
            List<decimal> temp2 = null;
            List<decimal> GPFContributions = null;
            List<decimal> GPFWithdrawals = null;
            if (Flow == "custom-tabs-three-gpfoutflow-tab")
            {
                temp2 = orders.Select(x => x.GPFWithdrawal).ToList();
            }
            else if (Flow == "custom-tabs-three-gpfinflow-tab")
            {
                temp2 = orders.Select(x => x.GPFContribution).ToList();
            }
            else
            {
                GPFContributions = orders.Select(x => x.GPFContribution).ToList();
                GPFWithdrawals = orders.Select(x => x.GPFWithdrawal).ToList();
            }

            return Json(new { _reports, Employers, temp2, GPFContributions, GPFWithdrawals });
        }

        public async Task<IActionResult> InvestmentManagement(String FinancialYear = null, String ReferenceNo = null, String InvestmentType = null)
        {
            try
            {

                var data = await _InvestmentDeclarationService.GetAllInvestment(FinancialYear, ReferenceNo, InvestmentType);

                ViewBag.TotalInvestment = data.AsEnumerable().Sum(a => a.InvestedAmount);
                return View(data);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        public IActionResult AddInvestment()
        {
            ViewData["Title"] = "Add Investment";

            DateTime dt = DateTime.Now;

            int CurrentYear = 0;
            int PrevYear = 0;
            int NextYear = 0;
            int CurrMonth = 0;
            int MostPrevYear = 0;
            CurrentYear = dt.Year;
            CurrMonth = dt.Month;
            PrevYear = dt.Year - 1;
            NextYear = dt.Year + 1;
            MostPrevYear = dt.Year - 2;
            List<String> yrs = new List<String>();
            yrs.Add(MostPrevYear.ToString() + "-" + PrevYear.ToString());
            yrs.Add(PrevYear.ToString() + "-" + CurrentYear.ToString());
            yrs.Add(CurrentYear.ToString() + "-" + NextYear.ToString());
            ViewBag.FinancialYear = yrs;

            CommonModel populateList = ReadJson.LoadJson();
            ViewBag.InvestmentTypeList = populateList.InvestmentTypeList;
            ViewBag.Mode = "Insert";
            return View("_InvestmentDeclaration", new InvestmentDeclarationModel());


        }

        public JsonResult AddEditInvestmentDeclaration(InvestmentDeclarationModel investmentDeclarationModel)
        {
            try
            {
                string result;
                var res = "";
                investmentDeclarationModel.InvestmentType = (string.IsNullOrEmpty(investmentDeclarationModel.Others) ? investmentDeclarationModel.InvestmentType : investmentDeclarationModel.Others);
                if (investmentDeclarationModel.ID == Guid.Empty)
                {
                    investmentDeclarationModel.CreatedBy = User.Identity.Name;
                    investmentDeclarationModel.ModifideBy = "";
                    result = _InvestmentDeclarationService.AddEditInvestmentDeclaration(investmentDeclarationModel, false);

                }
                else
                {
                    investmentDeclarationModel.ModifideBy = User.Identity.Name;
                    result = _InvestmentDeclarationService.AddEditInvestmentDeclaration(investmentDeclarationModel, true);

                }
                return Json(result);

            }
            catch (Exception ex)
            {
                return Json("Error");

            }
        }

        public IActionResult EditInvestment(Guid InvestmentId)
        {
            ViewData["Title"] = "Revise Investment Details";
            DateTime dt = DateTime.Now;

            int CurrentYear = 0;
            int PrevYear = 0;
            int NextYear = 0;
            int CurrMonth = 0;
            int MostPrevYear = 0;
            CurrentYear = dt.Year;
            CurrMonth = dt.Month;
            PrevYear = dt.Year - 1;
            NextYear = dt.Year + 1;
            MostPrevYear = dt.Year - 2;
            List<String> yrs = new List<String>();
            yrs.Add(MostPrevYear.ToString() + "-" + PrevYear.ToString());
            yrs.Add(PrevYear.ToString() + "-" + CurrentYear.ToString());
            yrs.Add(CurrentYear.ToString() + "-" + NextYear.ToString());
            ViewBag.FinancialYear = yrs;

            CommonModel populateList = ReadJson.LoadJson();
            ViewBag.InvestmentTypeList = populateList.InvestmentTypeList;

            ViewBag.InvestmentId = InvestmentId;
            ViewBag.Mode = "Update";
            var data = _InvestmentDeclarationService.GetInvestmentDetailsByInvestmentId(InvestmentId);
            return View("_InvestmentDeclaration", data);
        }

        public JsonResult DeleteInvestment(Guid InvestmentId)
        {
            var res = _InvestmentDeclarationService.DeleteInvestment(InvestmentId);
            return Json("Deleted");
        }


        public IActionResult CloseInvestment(Guid InvestmentId)
        {
            ViewData["Title"] = "Close Investment";
            DateTime dt = DateTime.Now;

            int CurrentYear = 0;
            int PrevYear = 0;
            int NextYear = 0;
            int CurrMonth = 0;
            int MostPrevYear = 0;
            CurrentYear = dt.Year;
            CurrMonth = dt.Month;
            PrevYear = dt.Year - 1;
            NextYear = dt.Year + 1;
            MostPrevYear = dt.Year - 2;
            List<String> yrs = new List<String>();
            yrs.Add(MostPrevYear.ToString() + "-" + PrevYear.ToString());
            yrs.Add(PrevYear.ToString() + "-" + CurrentYear.ToString());
            yrs.Add(CurrentYear.ToString() + "-" + NextYear.ToString());
            ViewBag.FinancialYear = yrs;

            CommonModel populateList = ReadJson.LoadJson();
            ViewBag.InvestmentTypeList = populateList.InvestmentTypeList;

            ViewBag.InvestmentId = InvestmentId;
            ViewBag.Mode = "Close";
            var data = _InvestmentDeclarationService.GetInvestmentDetailsByInvestmentId(InvestmentId);
            return View("_InvestmentDeclaration", data);
        }


        public ActionResult CloseInvestmentOpt(Guid InvestmentId, decimal ActualReturnOnInvestment)
        {
            var result = _InvestmentDeclarationService.CloseInvestment(InvestmentId, ActualReturnOnInvestment);
            return Json("Closed");
        }

        public IActionResult FinanceAnalysis()
        {
            DateTime dts = DateTime.Now;

            int CurrentYear = 0;
            int PrevYear = 0;
            int NextYear = 0;
            int CurrMonth = 0;
            int MostPrevYear = 0;
            CurrentYear = dts.Year;
            CurrMonth = dts.Month;
            PrevYear = dts.Year - 1;
            NextYear = dts.Year + 1;
            MostPrevYear = dts.Year - 2;
            List<String> yrs = new List<String>();
            yrs.Add(MostPrevYear.ToString() + "-" + PrevYear.ToString());
            yrs.Add(PrevYear.ToString() + "-" + CurrentYear.ToString());
            yrs.Add(CurrentYear.ToString() + "-" + NextYear.ToString());
            ViewBag.FinancialYear = yrs;

            CommonModel populateList = ReadJson.LoadJson();
            ViewBag.InvestmentTypeList = populateList.InvestmentTypeList;
            ViewBag.DTLOfficeList = populateList.DTLOfficeList;
            return View();
        }

        [HttpPost]
        public JsonResult GetFinancialAnalysisReport(string month = null, string DTLOffice = null, string Flow = null, string FinancialYear = null, string InvestmentType = null, string FinancialYears = null)
        {
            if (month.Length == 6)
                month = string.Format("0{0}", month);
            DateTime dt = Convert.ToDateTime(DateTime.ParseExact((string.Format("01/{0}", month)), "dd/MM/yyyy", CultureInfo.InvariantCulture));
            DateTime dcurrentyear = DateTime.Today.AddYears(1);
            DateTime dpreyear = DateTime.Today;
            var investmentdata = _InvestmentDeclarationService.GetAllInvestment(FinancialYears, null, InvestmentType).Result;
            var _budget = _BudgetDeclarationService.GetBudgetReport(FinancialYear);
            var _pension = _pensionManagement.GetPensionManagementReport(dt, DTLOffice, null, null);
            var _gpf = _gPFManagement.GetGPFManagementReport(dt, DTLOffice, null, null);
            var pensions = _pension.Where(x => x.Employer != "")
                            .GroupBy(x => x.Employer)
                            .Select(g => new
                            {
                                Employer = g.Key,
                                Contribution = g.Sum(x => x.Contribution),
                                PensionAmount = g.Sum(x => x.PensionAmount)
                            });

            var PensionEmployers = pensions.Select(x => x.Employer).ToList();
            var gpfs = _gpf.Where(x => x.Employer != "")
                            .GroupBy(x => x.Employer)
                            .Select(g => new
                            {
                                Employer = g.Key,
                                GPFContribution = g.Sum(x => x.GPFContribution),
                                GPFWithdrawal = g.Sum(x => x.GPFWithdrawal)
                            });

            var particulars = _budget.GroupBy(x => x.AllocationProgram)
                .Select(g => new
                {
                    AllocationProgram = g.Key,
                    BudgetAllocation = g.Sum(x => x.AllocatedFunds),
                    DisbursedAmount = g.Sum(x => x.DisbursedAmount)

                });

            var budgetparticulars = particulars.Select(x => x.AllocationProgram);

            var investments = investmentdata.GroupBy(x => x.InvestmentTitle).Select(
                g => new
                {
                    InvestmentTitle = g.Key,
                    InvestedAmount = g.Sum(x => x.InvestedAmount),
                    ExpectedROI = g.Sum(s => s.ExpectedROI)
                });

            var investmenttitles = investments.Select(x => x.InvestmentTitle);
            //_budget.Select(x => x.AllocationProgram);

            var GPFEmployers = pensions.Select(x => x.Employer).ToList();
            List<decimal> GPFContributions = null;
            List<decimal> GPFWithdrawals = null;
            List<decimal> Contributions = null;
            List<decimal> PensionAmounts = null;
            List<decimal> BudgetAllocations = null;
            List<decimal> BudgetExpenditure = null;
            List<decimal> Invested = null;
            List<decimal> ExpectedROI = null;

            GPFContributions = gpfs.Select(x => x.GPFContribution).ToList();
            GPFWithdrawals = gpfs.Select(x => x.GPFWithdrawal).ToList();
            Contributions = pensions.Select(x => x.Contribution).ToList();
            PensionAmounts = pensions.Select(x => x.PensionAmount).ToList();
            BudgetAllocations = particulars.Select(x => x.BudgetAllocation).ToList();
            BudgetExpenditure = particulars.Select(x => x.DisbursedAmount).ToList();
            Invested = investments.Select(x => x.InvestedAmount).ToList();
            ExpectedROI = investments.Select(x => x.ExpectedROI).ToList();

            return Json(new { _budget, _pension, _gpf, PensionEmployers, GPFEmployers, GPFContributions, GPFWithdrawals, Contributions, PensionAmounts, budgetparticulars, BudgetAllocations, BudgetExpenditure, investmentdata, investmenttitles, Invested, ExpectedROI });
        }

    }
}