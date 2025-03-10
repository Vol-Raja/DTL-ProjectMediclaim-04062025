using DTL.Business.Mediclaim.MedicalCard;
using DTL.Model.Models.Mediclaim;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DTL.API.Controllers.Mediclaim
{
    [Route("api/")]
    [ApiController]
    public class MedicalCardDetailsController : Controller
    {
        private readonly IMedicalCard _medicalCard;
        public MedicalCardDetailsController(IMedicalCard medicalCard)
        {
            _medicalCard = medicalCard;
        }
        [HttpPost]
        [Route("/save/medicalcarddetails")]
        public IActionResult SaveMedicalCard([FromBody] MedicalCardModel medicalCardModel)
        {
            int _medicalCardId = 0;
            try
            {
                if (ModelState.IsValid)
                {
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
    }
}
