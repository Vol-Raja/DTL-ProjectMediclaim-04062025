using DTL.Business.CMS.Testimony;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DTL.Website.Controllers
{

    public class TestimonyController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static ITestimonyService _addTestimonyService;

        public TestimonyController(ITestimonyService addTestimonyService)
        {
            _addTestimonyService = addTestimonyService;
        }
        // GET: TestimonyController


        public ActionResult DownloadFile(Guid TestimonyId, bool isView = true)
        {
            IEnumerable<TestimonyModel> Testimonys = _addTestimonyService.GetTestimonyModelByParam(TestimonyId, null);
            TestimonyModel TestimonyModel = Testimonys.FirstOrDefault();
            if (TestimonyModel == null) return Ok();

            byte[] fileBytes = TestimonyModel.Image;
            if (isView)
                Response.Headers.Add("Content-Disposition", "inline; filename=" + TestimonyModel.ImageName);
            return File(fileBytes, TestimonyModel.ImageContentType);

        }
    }
}
