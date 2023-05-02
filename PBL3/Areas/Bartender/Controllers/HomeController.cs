using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.Models;

namespace PBL3.Areas.Bartender.Controllers
{
    [Area("Bartender")]
    public class HomeController : Controller
    {
        private readonly TamtentoiContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public INotyfService _notifyService { get; }
        public HomeController(TamtentoiContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        public IActionResult Index()
        {
            var list = _context.DonDatMons.Where(x => x.TinhTrang == false).ToList(); // false: chưa làm, null: hủy

            return View(list);
        }
        [HttpPost]
        public IActionResult ShowDanhSach(int id)
        {

            var listOrderDetail = _context.MonDonDatMons.Where(x => x.DonDatMonId == id).ToList();
            List<Tuple<Mon, int>> listItem = new List<Tuple<Mon, int>>();
            foreach (var item in listOrderDetail)
            {
                var mon = _context.Mons.Find(item.MonId);
                if (mon != null)
                {
                    listItem.Add(new Tuple<Mon, int>(mon, (int)item.SoLuong));
                }
            }
            ViewBag.OrderId = id;
            ViewBag.ListOrderDetail = listOrderDetail;
            return PartialView("_PartialViewDanhSachMon", listItem);
        }
        public IActionResult ShowCongThuc(int id)
        {

            var mon = _context.Mons.Find(id);
            List<Tuple<NguyenLieu, int>> listCTDetails = new List<Tuple<NguyenLieu, int>>();
            if (mon != null)
            {
                var ListCT = _context.CongThucNguyenLieus.Where(x => x.CongThucId == mon.CongThucId).ToList();

                foreach (var item in ListCT)
                {
                    var nguyenLieu = _context.NguyenLieus.Find(item.NguyenLieuId);
                    listCTDetails.Add(new Tuple<NguyenLieu, int>(nguyenLieu, (int)item.SoLuong));
                }
            }
            return PartialView("_PartialViewShowCongThuc", listCTDetails);
        }
        public IActionResult ShowNguyenLieu()
        {

            var listNguyenLieu = _context.NguyenLieus.ToList();
            return PartialView("_PartialViewShowNguyenLieu", listNguyenLieu);
        }
        [HttpPost]
        public IActionResult HoanThanhDonHang(int monId, int orderId)
        {

            var mon = _context.Mons.Find(monId);
            string thongBao = "Thành công!";
            bool check = true;
            if (mon != null)
            {
                var ListCT = _context.CongThucNguyenLieus.Where(x => x.CongThucId == mon.CongThucId).ToList();
                var orderDetail = _context.MonDonDatMons.Where(x => x.DonDatMonId == orderId && x.MonId == monId).SingleOrDefault();

                foreach (var ctNl in ListCT)
                {
                    var nguyenLieuTrongKho = _context.NguyenLieus
                        .FirstOrDefault(x => x.NguyenLieuId == ctNl.NguyenLieuId);
                    if (nguyenLieuTrongKho != null && nguyenLieuTrongKho.SoLuong >= orderDetail.SoLuong * ctNl.SoLuong)
                    {
                        nguyenLieuTrongKho.SoLuong -= orderDetail.SoLuong * ctNl.SoLuong;
                        orderDetail.TinhTrang = true;
                    }
                    else
                    {
                        thongBao = "Nguyên liệu không đủ!";
                        check = false;
                        break;
                    }
                }
                _context.SaveChanges();
            }
            return Json(new { thongBao, check });
        }
        public IActionResult HuyDonHang(int monId, int orderId)
        {
            var orderDetail = _context.MonDonDatMons.Where(x => x.MonId == monId && x.DonDatMonId == orderId).FirstOrDefault();
            if (orderDetail != null)
            {
                orderDetail.TinhTrang = false;
                _context.SaveChanges();
            }
            return Json(new { });
        }
        public IActionResult ThongBaoPhucVu(int orderId)
        {
            var order = _context.DonDatMons.Find(orderId);
            if(order != null)
            {
                order.TinhTrang = true;
                _context.SaveChanges();
            }
            return RedirectToAction("index");
        }


	}
}
