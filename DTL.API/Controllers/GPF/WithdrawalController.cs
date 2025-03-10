using DTL.Business.GPF.Withdrawal;
using DTL.Model.Models.GPF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.API.Controllers.GPF
{
    [Route("api/")]
    [ApiController]
    public class WithdrawalController : ControllerBase
    {
        private readonly IWithdrawal _withdrawal;

        public WithdrawalController(IWithdrawal withdrawal)
        {
            _withdrawal = withdrawal;
        }

        #region Refundable
        /// <summary>
        /// Save Refundabe GPF detail
        /// </summary>
        /// <param name="withdrawalModel"></param>
        /// <returns>INT</returns>
        [HttpPost]
        [Route("/gpf/save/refundable")]
        public IActionResult SaveRefundableGPF([FromBody] GPFWithdrawalModel withdrawalModel)
        {
            string _withdrawalId = "";
            try
            {
                if (ModelState.IsValid)
                {   
                    var id = _withdrawal.SaveGPFWithdrawal(withdrawalModel);
                    if (withdrawalModel.OperationType.ToLower() == "submit")
                    {
                        _withdrawalId = _withdrawal.GetGPFWithdrawalByParam(withdrawalModel.WithdrawId, null, null, null, null, null, null, null).FirstOrDefault().UniqueNumber;
                    }
                    else
                    {
                        _withdrawalId = id.ToString();
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
            return Ok(_withdrawalId);
        }
        #endregion

        #region Non-Refundable
        /// <summary>
        /// Save Non-Refundabe GPF detail
        /// </summary>
        /// <param name="withdrawalModel"></param>
        /// <returns>INT</returns>
        [HttpPost]
        [Route("/gpf/Save/non-refundable")]
        public IActionResult SaveNonRefundableGPF([FromBody] GPFWithdrawalModel withdrawalModel)
        {
            string _withdrawalId = "";
            try
            {
                if (ModelState.IsValid)
                {   
                    var id = _withdrawal.SaveGPFWithdrawal(withdrawalModel);
                    if (withdrawalModel.OperationType.ToLower() == "submit")
                    {
                        _withdrawalId = _withdrawal.GetGPFWithdrawalByParam(withdrawalModel.WithdrawId, null, null, null, null, null, null, null).FirstOrDefault().UniqueNumber;
                    }
                    else
                    {
                        _withdrawalId = id.ToString();
                    }
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

            return Ok(_withdrawalId);
        }
        #endregion
    }
}
