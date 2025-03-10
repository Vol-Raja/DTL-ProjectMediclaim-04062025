using DTL.Business.GPF.Processing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.API.Controllers.GPF
{
    [Route("api/")]
    [ApiController]
    public class ProcessingController : ControllerBase
    {
        private readonly IProcessing _processing;

        public ProcessingController(IProcessing processing)
        {
            _processing = processing;
        }

        [HttpGet]
        [Route("/gpf/monthly-statement/{processingDate?}/{employer?}/{employeeId?}")]
        public IActionResult GetMonthlyStatement([FromRoute] string processingDate, [FromRoute] string employer = null, [FromRoute] string employeeId = null)
        {
            //1%2F1%2F0001

            employer = employer != "NA" ? employer : null;
            var month = !string.IsNullOrEmpty(processingDate) ? (int.TryParse(processingDate.Split('-')[0].Trim(), out var i) ? (int?)i : null) : null;
            var year = !string.IsNullOrEmpty(processingDate) ? (int.TryParse(processingDate.Split('-')[1].Trim(), out var j) ? (int?)j : null) : null;
            employeeId = employeeId != "0" ? employeeId : null;

            var gpfProcessingModel = _processing.GetDetailByParam(employer, month, year, employeeId, null).FirstOrDefault();
            return Ok(gpfProcessingModel);
        }

        [HttpGet]
        [Route("/gpf/yearly-statement/{processingDate?}/{employer?}/{employeeId?}")]
        public IActionResult GetYearlyStatement([FromRoute] string processingDate, [FromRoute] string employer = null, [FromRoute] string employeeId = null)
        {
            //1%2F1%2F0001

            employer = employer != "NA" ? employer : null;
            var month = !string.IsNullOrEmpty(processingDate) ? (int.TryParse(processingDate.Split('-')[0].Trim(), out var i) ? (int?)i : null) : null;
            var year = !string.IsNullOrEmpty(processingDate) ? (int.TryParse(processingDate.Split('-')[1].Trim(), out var j) ? (int?)j : null) : null;
            employeeId = employeeId != "0" ? employeeId : null;

            var gpfProcessingModel = _processing.GetDetailByParam(employer, month, year, employeeId, null);
            return Ok(gpfProcessingModel);
        }
    }
}
