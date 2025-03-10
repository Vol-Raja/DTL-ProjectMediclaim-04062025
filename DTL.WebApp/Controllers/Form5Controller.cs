using DTL.Business.Form5;
using DTL.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DTL.WebApp.Controllers
{
    public class Form5Controller : Controller
    {
        private static IForm5 _iForm5;

        public Form5Controller(IForm5 form5)
        {
            _iForm5 = form5;
        }
        // GET: Form5Controller
        public ActionResult Index()
        {
            return View();
        }

        // GET: Form5Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Form5Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        

        // GET: Form5Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Form5Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: Form5Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Form5Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
        [HttpPost]
        public async Task<IActionResult> AddDocument(IEnumerable<IFormFile> DocumentPhotos)
        {
            Form5Model form5Model = new Form5Model();
            if (DocumentPhotos != null)
            {
               
                foreach (var item in DocumentPhotos)
                {

                    using (MemoryStream stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        form5Model.exampleInputFileAadhar = stream.ToArray();
                       // documentModel.DocumentPhoto = stream.ToArray();
                    }
                  
                }
            }
            
           return PartialView("Documents", form5Model);
        }
    }
}
