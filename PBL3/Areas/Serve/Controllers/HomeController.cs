using Microsoft.AspNetCore.Mvc;

namespace PBL3.Areas.Serve.Controllers
{
    [Area("Serve")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
