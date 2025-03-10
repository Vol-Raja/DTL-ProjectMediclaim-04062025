using DTL.Business.Common;
using DTL.Business.Mediclaim.MedicalCard;
using DTL.Business.UserManagement;
using DTL.Model.Models.Mediclaim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    [Area("Mediclaim")]
    [Authorize(Roles = "Hospitals,DVB Pension Trust")]
    public class MedicalCardController : Controller
    {
        private readonly IMedicalCard _medicalCard;
        private readonly IAssignPermission _assignPermission;
        private bool _create, _edit, _view, _delete = false;
        public MedicalCardController(IMedicalCard medicalCard, IAssignPermission assignPermission)
        {
            _medicalCard = medicalCard;
            _assignPermission = assignPermission;
        }
        public IActionResult Index()
        {
            try
            {
                var _medicalCardList = _medicalCard.GetMedicalCardsByCreatedBy(User.Identity.Name);
                GetPermissions();
                ViewBag.Create = _create;
                ViewBag.Edit = _edit;
                ViewBag.View = _view;
                ViewBag.Delete = _delete;
                return View(_medicalCardList);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult AddNewMedicalCard()
        {
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList;
            return View();
        }
        public IActionResult MediclaimCardView() {
            return View();
        }

        [HttpPost]
        public IActionResult SaveMedicalCard([FromBody] MedicalCardModel medicalCardModel) 
        {
            int _medicalCardId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    medicalCardModel.CreatedBy = User.Identity.Name;
                    _medicalCardId = _medicalCard.SaveMedicalCard(medicalCardModel);
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

            return Ok(_medicalCardId);
        }


        [Route("Mediclaim/MedicalCard/PrintPreview/{medicalCardId}")]
        public IActionResult PrintPreview([FromRoute] int medicalCardId)
        {
            var _voucherModel = _medicalCard.GetMedicalCardsByParam(medicalCardId, null, null, null);
            return View(_voucherModel.FirstOrDefault());
        }

        [Route("/Mediclaim/MedicalCard/Delete/{medicalCardId}")]
        public IActionResult UpdateMedicalCardIsDelete([FromRoute] int medicalCardId)
        {
            var returnValue = 0;
            MedicalCardModel medicalCardModel = new MedicalCardModel();
            try
            {
                if (ModelState.IsValid)
                {
                    medicalCardModel.MedicalCardId = medicalCardId;
                    medicalCardModel.ModifideBy = User.Identity.Name;
                    medicalCardModel.IsDelete = true;
                    returnValue = _medicalCard.DeleteMedicalCard(medicalCardModel);
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
            return Redirect("/Mediclaim/MedicalCard");
        }



        [Route("Mediclaim/MedicalCard/EditMedicalCard/{medicalCardId}")]
        public IActionResult EditMedicalCard([FromRoute] int medicalCardId)
        {
            var data = ReadJson.LoadJson();
            ViewBag.DTLOfficeList = data.DTLOfficeList;
            ViewBag.MedicalCardId = medicalCardId;
            return View();
        }

        [Route("Mediclaim/MedicalCard/GetMedicalCardForEdit/{medicalCardId}")]
        [HttpGet]
        public IActionResult GetMedicalCardForEdit([FromRoute] int medicalCardId)
        {
            var _voucherModel = _medicalCard.GetMedicalCardsByParam(medicalCardId, null, null, null);
            return Json(_voucherModel.FirstOrDefault());
        }

        [Route("/Mediclaim/MedicalCard/Edit")]
        [HttpPost]
        public IActionResult EditMedicalCard([FromBody] MedicalCardModel medicalCardModel)
        {
            int _rowCount = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    medicalCardModel.ModifideBy = User.Identity.Name;
                    _rowCount = _medicalCard.UpdateMedicalCard(medicalCardModel);
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
            var permission = _assignPermission.GetAssignPermissionByParam(User.Identity.Name, "Mediclaim", "Mediclaim Card Details").FirstOrDefault();
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
