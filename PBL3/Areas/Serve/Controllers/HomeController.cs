using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PBL3.Model;
using PBL3.Models;

namespace PBL3.Areas.Serve.Controllers
{
    [Area("Serve")]
    public class HomeController : Controller
    {
        private readonly TamtentoiContext _context = new TamtentoiContext();
        
        public IActionResult Index()
        {
            var list = _context.Mons.OrderBy(n => n.MonId).ToList();
            return View(list);
        }
    }
}
