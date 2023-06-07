using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.BLL;
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

        public IActionResult Index(int id =0)
        {
            var list = _context.DonDatMons.Where(x => x.TinhTrang == false).ToList(); // false: chưa làm, null: hủy
            ViewBag.OrderId = id;
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
                var orderDetail = _context.MonDonDatMons.Where(x => x.DonDatMonId == orderId && x.MonId == monId && x.TinhTrang == 0).SingleOrDefault();
                orderDetail.TinhTrang = 1;
                foreach (var ctNl in ListCT)
                {
                    var nguyenLieuTrongKho = _context.NguyenLieus
                        .FirstOrDefault(x => x.NguyenLieuId == ctNl.NguyenLieuId);
                    if (nguyenLieuTrongKho != null && nguyenLieuTrongKho.SoLuong >= orderDetail.SoLuong * ctNl.SoLuong)
                    {
                        nguyenLieuTrongKho.SoLuong -= orderDetail.SoLuong * ctNl.SoLuong;
                        
                    }
                    else 
                    {
                        thongBao = "Nguyên liệu không đủ!";
                        orderDetail.TinhTrang = 0;
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
            var orderDetail = _context.MonDonDatMons.Where(x => x.MonId == monId && x.DonDatMonId == orderId && x.TinhTrang == 0).FirstOrDefault();
            if (orderDetail != null)
            {
                orderDetail.TinhTrang = 2;
                _context.SaveChanges();
            }
            return Json(new { });
        }
        public IActionResult ThongBaoPhucVu(int orderId)
        {
            if (ServicePhucVu.GetOrderDetails(orderId).Any(item => item.Item3 == 0))
            {
                _notifyService.Warning("Vui lòng xử lí tất cả các món!");
                return RedirectToAction("index", new { id = orderId });
            }
            else
            {
                var order = _context.DonDatMons.Find(orderId);
                if (order != null)
                {
                    order.TinhTrang = true;
                    _context.SaveChanges();
                }
            }
			_notifyService.Success("Thông báo thành công");
			return RedirectToAction("index");

        }
    }
}
