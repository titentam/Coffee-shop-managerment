using AspNetCore;
using DiChoSaiGon.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (HttpContext.Session.GetInt32("user")==null) return RedirectToAction("index", "Login", new { area = "" });

            // thong ke ngay trong thang hien tai
            var statistic = _context.StatisticByMonth().Where(x => x.Year == 2023).ToList();

            var currentRevenue = _context.StatisticByMonth()
                .Where(x => x.Month == DateTime.Now.Month && x.Year == DateTime.Now.Year)
                .SingleOrDefault();

            ViewBag.currentRevenue = "0";
            if (currentRevenue != null)
            {
                ViewBag.currentRevenue = currentRevenue.Total.ToString("#,##0");
            }

            // get tong nhan vien, tong mon
            ViewBag.totalItem = _context.Mons.Count();
            ViewBag.totalStaff = _context.NhanViens.Count();


            // thong ke loai mon
            var listLoaiMon = _context.LoaiMons.Select(x => new { TenLoaiMon = x.TenLoaiMon, Count = x.Mons.Count }).ToList();
            List<string> tenLoaiMon = new List<string>();
            List<int> soLuong = new List<int>();

            foreach (var mon in listLoaiMon)
            {
                tenLoaiMon.Add(mon.TenLoaiMon==null?"" :mon.TenLoaiMon);
                soLuong.Add(mon.Count);
            }

            ViewBag.tenLoaiMon = tenLoaiMon;
            ViewBag.soLuong = soLuong;

            // thong ke Top mon trong tuan
            var listMon = _context.StatisticByMonId();
            if(listMon != null)
            {
                List<Tuple<Mon,int, int>> tmp = new List<Tuple<Mon,int,int>>();   
                foreach(var item in listMon)
                {
                    // tim mon
                    var mon = _context.Mons.Find(item.MonId);
                    int soLuongCon = int.MaxValue;
                    
                    if(mon != null)
                    {
                        // tim danh sach nguyen lieu
                        var listNguyenLieu = _context.CongThucNguyenLieus.Where(x => x.CongThucId == (mon.CongThucId ?? 0)).ToList();
                        foreach(var itemNguyenLieu in listNguyenLieu)
                        {
                            var nguyenLieu = _context.NguyenLieus.Find(itemNguyenLieu.NguyenLieuId);
                            if (nguyenLieu != null)
                            {
                                soLuongCon = Math.Min(soLuongCon, nguyenLieu.SoLuong ?? 0 / itemNguyenLieu.SoLuong ?? 1);
                            }
                            else
                            {
                                soLuongCon = 0;
                            }
                        }
                        tmp.Add(new Tuple<Mon, int,int>(mon, item.SoLuong, soLuongCon));
                    }
                }
                ViewBag.ListMon = tmp;
            }
            return View(statistic);
        }
    }
}
