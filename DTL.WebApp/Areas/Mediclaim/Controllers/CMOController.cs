using Microsoft.AspNetCore.Mvc;

namespace DTL.WebApp.Areas.Mediclaim.Controllers
{
    public class CMOController : Controller
    {
        [Area("Mediclaim")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
