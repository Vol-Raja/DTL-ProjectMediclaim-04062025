using DTL.Business.ApprovalLadder;
using DTL.Business.Mediclaim.Processing;
using DTL.Model.Models.ApprovalLadder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Areas.ApprovalLadder
{
    [Area("ApprovalLadder")]
    public class ApprovalLadderController : Controller
    {
        private readonly IApprovalLadder _approvalLadder;
        private readonly IProcessing _mediclaimProcessing;

        public ApprovalLadderController(IApprovalLadder approvalLadder, IProcessing mediclaimProcessing)
        {
            _approvalLadder = approvalLadder;
            _mediclaimProcessing = mediclaimProcessing;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/ApprovalLadder/ChangeApprover")]
        public IActionResult ChangeApprover([FromBody] ApprovalLadderModel model)
        {
            int _ladderId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedBy = User.Identity.Name;
                    if (model.Module.ToLower() == "mediclaimcashless") {
                        var mediclaimDetail = _mediclaimProcessing.GetCashlessByClaimId(model.ReferenceId);
                        model.Comment = mediclaimDetail.RejectReason;
                        _ladderId = _approvalLadder.SaveApprovalLadder(model);
                        if (_ladderId > 0) {
                            _approvalLadder.UpdateForwardTo(model);
                        }
                    }                
                }
                else
                {
                    IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return BadRequest(allErrors);
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            return Ok(_ladderId);
        }
    }
}
