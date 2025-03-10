using DTL.Business.Mediclaim.MedicalPageDetail;
using DTL.Business.UserManagement;
using DTL.Model.Models.Mediclaim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    [Authorize(Roles = "Hospitals,DVB Pension Trust")]
    public class MedicalPageDetailController : Controller
    {
        private readonly IMedicalPageDetail _medicalPageDetail; 
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;

        public MedicalPageDetailController(IMedicalPageDetail medicalPageDetail, IAssignPermission assignPermission)
        {
            _medicalPageDetail = medicalPageDetail;
            _assignPermission = assignPermission;
        }

        /// <summary>
        /// Get the detail of medical page
        /// </summary>
        /// <returns>List</returns>
        public IActionResult Index()
        {
            var _details = _medicalPageDetail.GetMedicalPageDetailsByParam(null,null,null,null,null,User.Identity.Name);
            GetPermissions();
            ViewBag.Create = _create;
            ViewBag.Edit = _edit;
            ViewBag.View = _view;
            ViewBag.Delete = _delete;
            return View(_details);
        }

        public IActionResult AddNewMedicalPageDetail()
        {
            return View();
        }

        /// <summary>
        /// Get the detail of medical page by param
        /// </summary>
        /// <param name="employeeNumber"></param>
        /// <param name="ppoNumber"></param>
        /// <param name="department"></param>
        /// <param name="pageNumber"></param>
        /// <returns>list</returns>
        [Route("Mediclaim/MedicalPageDetail/MedicalPageDetailsByParam/{employeeNumber}/{ppoNumber?}/{department?}/{pageNumber?}")]
        public IActionResult GetMedicalPageDetailsByParam([FromRoute] string employeeNumber, [FromRoute] string ppoNumber, [FromRoute] string department, [FromRoute] int? pageNumber)
        {
            //1%2F1%2F0001
            pageNumber = pageNumber > 0 ? pageNumber : null;
            var _details = _medicalPageDetail.GetMedicalPageDetailsByParam(null, employeeNumber, ppoNumber, department, pageNumber, null);
            return Json(_details);
        }

        /// <summary>
        /// Get the detail of medical page by param
        /// </summary>
        /// <param name="pageDetailId"></param>
        /// <returns>list</returns>
        [Route("Mediclaim/MedicalPageDetail/PrintPreview/{pageDetailId}")]
        public IActionResult PrintPreview([FromRoute] int pageDetailId)
        {
            //1%2F1%2F0001
            //pageNumber = pageNumber > 0 ? pageNumber : null;
            var _details = _medicalPageDetail.GetMedicalPageDetailsByParam(pageDetailId, null, null, null, null, null).FirstOrDefault();
            return View(_details);
        }

        /// <summary>
        /// Save detail in db
        /// </summary>
        /// <returns>Int</returns>
        [HttpPost]
        public IActionResult SaveNewMedicalPageDetail([FromBody] MedicalPageDetailModel medicalPageDetailModel)
        {
            int _pageDetailId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    medicalPageDetailModel.CreatedBy = User.Identity.Name;
                    _pageDetailId = _medicalPageDetail.SaveMedicalPageDetail(medicalPageDetailModel);
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

            return Ok(_pageDetailId);
        }

        [Route("/Mediclaim/MedicalPageDetail/Delete/{pageDetailId}")]
        public IActionResult UpdateMedicalPageIsDelete([FromRoute] int pageDetailId)
        {
            var returnValue = 0;
            MedicalPageDetailModel medicalPageDetailModel = new MedicalPageDetailModel();
            try
            {
                if (ModelState.IsValid)
                {
                    medicalPageDetailModel.PageDetailId = pageDetailId;
                    medicalPageDetailModel.ModifideBy = User.Identity.Name;
                    medicalPageDetailModel.IsDelete = true;
                    returnValue = _medicalPageDetail.DeleteMedicalPageDetail(medicalPageDetailModel);
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

            //return Json(returnValue > 0 ? "Success" : "Fail");
            return Redirect("/Mediclaim/MedicalPageDetail");
        }


        [Route("/Mediclaim/MedicalPageDetail/EditMedicalPageDetail/{pageDetailId}")]
        [HttpGet]
        public IActionResult EditMedicalPageDetail([FromRoute]int pageDetailId)
        {
            ViewBag.PageDetailId = pageDetailId;
            return View();
        }

        [Route("/Mediclaim/MedicalPageDetail/GetMedicalPageDetailForEdit/{pageDetailId}")]
        [HttpGet]
        public IActionResult GetMedicalPageDetailForEdit([FromRoute] int pageDetailId)
        {
            var _details = _medicalPageDetail.GetMedicalPageDetailsByParam(pageDetailId, null, null, null, null, null).FirstOrDefault();
            return Json(_details);
        }

        [Route("/Mediclaim/MedicalPageDetail/Edit")]
        [HttpPost]
        public IActionResult UpdateMedicalPageDetail([FromBody] MedicalPageDetailModel medicalPageDetailModel)
        {
            int _rowCount = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    medicalPageDetailModel.ModifideBy = User.Identity.Name;
                    _rowCount = _medicalPageDetail.UpdateMedicalPageDetail(medicalPageDetailModel);
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

            return Ok(_rowCount);
        }

        private void GetPermissions()
        {
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "Mediclaim", "Medical Page Details").FirstOrDefault();
            if (permission != null)
            {
                _create = permission.Create;
                _edit = permission.Edit;
                _view = permission.View;
                _delete = permission.Delete;
            }
        }
        public ActionResult OPDMedicalPageDetails()
        {
            return View();
        }
        public ActionResult MedicalPageDetailView() {
            return View();
        }

    }
}
