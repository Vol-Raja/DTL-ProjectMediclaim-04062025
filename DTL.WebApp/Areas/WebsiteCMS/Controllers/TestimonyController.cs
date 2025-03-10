using DocumentFormat.OpenXml.Drawing.Charts;
using DTL.Business.CMS.Testimony;
using DTL.Model.Models;
using DTL.Model.Models.CMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DTL.WebApp.Areas.WebsiteCMS.Controllers
{
    [Area(StringConstants.CMS_AREA)]
    [Authorize(Roles = "SuperAdmin")]
    public class TestimonyController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static ITestimonyService _addTestimonyService;

        public TestimonyController(ITestimonyService addTestimonyService)
        {
            _addTestimonyService = addTestimonyService;
        }
        // GET: TestimonyController
        public ActionResult Index(bool isArchieved = false)
        {
            IEnumerable<TestimonyModel> TestimonyModels = _addTestimonyService.GetTestimonyModelByParam(null, isArchieved);
            return View(TestimonyModels.OrderByDescending(x => x.CreatedDate));

        }
        public ActionResult Dashboard(bool isArchieved = false)
        {
            return View();

        }
        // GET: TestimonyController/Details/5
        public ActionResult Details(Guid id, bool isView = false)
        {
            IEnumerable<TestimonyModel> Testimonys = _addTestimonyService.GetTestimonyModelByParam(id, null);
            TestimonyModel TestimonyModel = Testimonys.FirstOrDefault();
            //    TestimonyModel.ViewOnly = isView;
            TestimonyModel.IsNew = false;
            return View("AddEditTestimony", TestimonyModel);
        }

        // GET: TestimonyController/Create
        public ActionResult Create()
        {
            TestimonyModel TestimonyModel = new()
            {
                IsNew = true
            };
            return View("AddEditTestimony", TestimonyModel);
        }

        // POST: TestimonyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TestimonyController/Edit/5
        public ActionResult Edit(Guid id)
        {
            IEnumerable<TestimonyModel> Testimonys = _addTestimonyService.GetTestimonyModelByParam(id, null);
            TestimonyModel TestimonyModel = Testimonys.FirstOrDefault();

            TestimonyModel.IsNew = false;
            return View("AddEditTestimony", TestimonyModel);
        }


        // GET: TestimonyController/Delete/5
        [HttpPost]
        public ActionResult Delete([FromBody] Guid Id)
        {
            IEnumerable<TestimonyModel> Testimonys = _addTestimonyService.GetTestimonyModelByParam(Id, null);
            TestimonyModel TestimonyModel = Testimonys.FirstOrDefault();
            TestimonyModel.IsDeleted = true;
            var result = _addTestimonyService.AddEditTestimonyData(TestimonyModel, true);
            //  return RedirectToAction(nameof(Index));
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TestimonyModel TestimonyModel)
        {
            try
            {
                if (TestimonyModel.ID == System.Guid.Empty)
                    TestimonyModel.CreatedBy = User.Identity.Name;
                else
                    TestimonyModel.ModifideBy = User.Identity.Name;

                if (Request.Form.Files.Count > 0)
                    foreach (var file in Request.Form.Files)
                    {
                        using var fs1 = file.OpenReadStream();
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        switch (file.Name)
                        {

                            case "Image":
                                TestimonyModel.Image = ms1.ToArray();
                                TestimonyModel.ImageName = file.FileName;
                                TestimonyModel.ImageContentType = file.ContentType;
                                break;
                        }
                    }

                string result = _addTestimonyService.AddEditTestimonyData(TestimonyModel, TestimonyModel.ID != Guid.Empty);
                if (result == "add" || result == "update")
                {
                    TempData["Message"] = "Changes are saved";
                    return Json(result);
                    //return RedirectToAction(nameof(Index)); //Json(result);
                }
                else
                    throw new Exception(result);
            }
            catch (Exception ex)
            {
                log.Error("Testimony Save", ex);
                //return View("Error");
                return Json(ex);

            }
        }

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
