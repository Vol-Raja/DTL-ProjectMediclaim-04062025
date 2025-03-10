using DTL.Business.Mediclaim.MediclaimRaise;
using DTL.Model.Models.Mediclaim;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DTL.API.Controllers.Mediclaim
{
    [Route("api")]
    [ApiController]
    public class RaiseController : ControllerBase
    {
        private readonly IRaise _raise;
        public RaiseController(IRaise raise)
        {
            _raise = raise;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noncashlessModel"></param>
        /// <returns>OK</returns>
        [Route("/save/noncashless")]
        [HttpPost]
        public IActionResult AddNewMediclaimNonCashless([FromBody] NonCashlessModel noncashlessModel)
        {
            int _mediclaimId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    _mediclaimId = _raise.SaveMediclaimNonCashless(noncashlessModel);
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

            return Ok(_mediclaimId);
        }

    }
}
