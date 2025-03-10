using DTL.Business.Mediclaim.MedicalPageDetail;
using DTL.Model.Models.Mediclaim;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.API.Controllers.Mediclaim
{
    [Route("api")]
    [ApiController]
    public class PageDetailsController : Controller
    {
        private readonly IMedicalPageDetail _medicalPageDetail;
        public PageDetailsController(IMedicalPageDetail medicalPageDetail)
        {
            _medicalPageDetail = medicalPageDetail;
        }

        /// <summary>
        /// Save detail in db
        /// </summary>
        /// <returns>Int</returns>
        [HttpPost]
        [Route("/save/pagedetails")]
        public IActionResult AddNewMedicalPageDetail([FromBody] MedicalPageDetailModel medicalPageDetailModel)
        {
            int _pageDetailId = 0;
            try
            {
                if (ModelState.IsValid)
                {
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
    }
}
