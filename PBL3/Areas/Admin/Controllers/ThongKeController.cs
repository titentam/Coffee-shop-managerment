using Microsoft.AspNetCore.Mvc;
using PBL3.Models;

namespace PBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        private readonly TamtentoiContext _context;
        public ThongKeController(TamtentoiContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("user") == null) return RedirectToAction("index", "Login", new { area = "" });

            var statistic = _context.StatisticByMonth().Where(x => x.Year == 2023).ToList();

            var currentRevenue = _context.StatisticByMonth()
                .Where(x => x.Month == DateTime.Now.Month && x.Year == DateTime.Now.Year)
                .SingleOrDefault();

            ViewBag.currentRevenue = "0";
            if (currentRevenue != null)
            {
                ViewBag.currentRevenue = currentRevenue.Total.ToString("#,##0");
            }

            ViewBag.totalItem = _context.Mons.Count();
            ViewBag.totalStaff = _context.NhanViens.Count();

            var listLoaiMon = _context.LoaiMons.Select(x => new { TenLoaiMon = x.TenLoaiMon, Count = x.Mons.Count }).ToList();
            List<string> tenLoaiMon = new List<string>();
            List<int> soLuong = new List<int>();

            foreach (var mon in listLoaiMon)
            {
                tenLoaiMon.Add(mon.TenLoaiMon == null ? "" : mon.TenLoaiMon);
                soLuong.Add(mon.Count);
            }

            ViewBag.tenLoaiMon = tenLoaiMon;
            ViewBag.soLuong = soLuong;

            return View(statistic);
        }
        
    }
}
