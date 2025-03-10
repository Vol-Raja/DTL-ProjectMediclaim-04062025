using DTL.Business.CMS.Feedback;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DTL.Website.Controllers
{
    public class FeedbackController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IFeedbackService _addFeedbackService;

        public FeedbackController(IFeedbackService addFeedbackService)
        {
            _addFeedbackService = addFeedbackService;
        }
        // GET: FeedbackController
        public ActionResult Index(bool isArchieved = false)
        {
            //IEnumerable<FeedbackModel> Feedbacks = _addFeedbackService.GetFeedbackModelByParam(null, isArchieved);
            return View();
        }

        // GET: FeedbackController/Details/5
        public ActionResult Details(Guid id)
        {

            IEnumerable<FeedbackModel> Feedbacks = _addFeedbackService.GetFeedbackModelByParam(id, null);
            FeedbackModel FeedbackModel = Feedbacks.FirstOrDefault();
            FeedbackModel.ViewOnly = true;
            FeedbackModel.IsNew = false;
            return View("AddEditFeedback", FeedbackModel);
        }

        // GET: FeedbackController/Create
        public ActionResult Create()
        {
            FeedbackModel FeedbackModel = new();
            FeedbackModel.IsNew = true;
            return View("AddEditFeedback", FeedbackModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(FeedbackModel FeedbackModel)
        {
            try
            {
                if (FeedbackModel.ID == Guid.Empty)
                    FeedbackModel.CreatedBy = User.Identity.Name;
                else
                    FeedbackModel.ModifideBy = User.Identity.Name;

                if(Convert.ToString(TempData["CaptchaCode"]) != FeedbackModel.CaptchaCode)
                {
                    return Json("InvalidCaptchaCode");
                }
                string result = _addFeedbackService.AddEditFeedbackData(FeedbackModel, FeedbackModel.ID != System.Guid.Empty);
                if (result == "add" || result == "update")
                    return Json(1);
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("Feedback Save", ex);
                //return View("Error");
                return Json(ex.Message);

            }
        }
   
    }
}
