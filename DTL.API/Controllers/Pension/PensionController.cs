using DTL.Business.GeneratePension;
using Microsoft.AspNetCore.Mvc;

namespace DTL.API.Controllers.PensionCertificate
{
    [Route("api/")]
    [ApiController]
    public class PensionController : ControllerBase
    {
        private readonly IGeneratePension _generatePension;

        public PensionController(IGeneratePension generatePension)
        {
            _generatePension = generatePension;
        }

        [HttpGet]
        [Route("pension/certificate/{month?}/{year?}/{bank?}/{employer?}/{ppono?}")]
        public IActionResult GetGeneratePensionByParam([FromRoute] int? month, [FromRoute] int? year, [FromRoute] int? ppono, [FromRoute] string bank = null, [FromRoute] string employer = null)
        {
            //var _model = new GeneratePensionViewModel();
            employer = employer != "NA" ? employer : null;
            month = month > 0 ? month : null;
            year = year > 0 ? year : null;
            ppono = ppono > 0 ? ppono : null;
            bank = bank != "NA" ? bank : null;

            var _generatePensionList = _generatePension.GetGeneratePension(month, year, bank, employer, ppono);
            return Ok(_generatePensionList);
        }

        [HttpGet]
        [Route("Pension/slip/{month?}/{year?}/{ppono?}")]
        public IActionResult GetPensionSlipCertificateByParam([FromRoute] int? month, [FromRoute] int? year, [FromRoute] int? ppono)
        {
            month = month > 0 ? month : null;
            year = year > 0 ? year : null;
            ppono = ppono > 0 ? ppono : null;

            var _generatePensionList = _generatePension.GetGeneratePension(month, year, null, null, ppono);
            return Ok(_generatePensionList);
        }
    }
}
