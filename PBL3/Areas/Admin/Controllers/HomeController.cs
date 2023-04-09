using AspNetCore;
using DiChoSaiGon.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.Model;
using PBL3.Models;

namespace PBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        
        private readonly TamtentoiContext _context;
        public HomeController(TamtentoiContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("user") == null) return RedirectToAction("index", "Login", new {area=""});
            var nv = _context.NhanViens.Include(n => n.LoaiNvNavigation)
                                       .Where(n => n.NhanVienId == HttpContext.Session.GetInt32("user")).SingleOrDefault();
            return View(nv);
        }
    }
}
