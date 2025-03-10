using DTL.Business.Grievance;
using DTL.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class GrievanceController : ControllerBase
    {
        private readonly IAddGrievance _grievanceService;
        public GrievanceController(IAddGrievance grievanceService)
        {
            _grievanceService = grievanceService;
        }

        [HttpPost]
        [Route("/save/grievance")]
        public IActionResult SaveGrievance([FromBody]GrievanceModel grievanceModel)
        {
            try
            {
                string result;
                grievanceModel.ModifideBy = "";
                result = _grievanceService.AddEditGrievance(grievanceModel, false);
                if (result == "add" || result == "update") {
                    return Ok("success");
                }
                else {
                    return NoContent();
                }
                
            }
            catch (Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

        [HttpPost]
        [Route("/update/grievance")]
        public IActionResult UpdateGrievance([FromBody]GrievanceModel grievanceModel)
        {
            try
            {
                string result;
                result = _grievanceService.AddEditGrievance(grievanceModel, true);
                if (result == "add" || result == "update") {
                    return Ok("success");
                }
                else {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
    }
}
