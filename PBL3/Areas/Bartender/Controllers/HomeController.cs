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
            var list = _context.DonDatMons.Where(x=>x.TinhTrang==false ).ToList();

            return View(list);
        }
        [HttpPost]
        public IActionResult ShowDanhSach(int id)
        {
            
            var listOrderDetail = _context.MonDonDatMons.Where(x=>x.DonDatMonId==id ).ToList();
            List<Tuple<Mon, int>> listItem = new List<Tuple<Mon, int>>();
            foreach(var item in listOrderDetail)
            {
                var mon = _context.Mons.Find(item.MonId);
                if(mon!=null)
                {
                    listItem.Add(new Tuple<Mon,int>(mon,(int)item.SoLuong));
                }
            }
            return PartialView("_PartialViewDanhSachMon", listItem);
        }
        public IActionResult ShowCongThuc(int id)
        {

            var mon = _context.Mons.Find(id);
            List<Tuple<NguyenLieu, int>> listCTDetails = new List<Tuple<NguyenLieu, int>>();
            if (mon!=null)
            {
                var ListCT = _context.CongThucNguyenLieus.Where(x => x.CongThucId==mon.CongThucId).ToList();
                
                foreach (var item in ListCT)
                {
                    var nguyenLieu=_context.NguyenLieus.Find(item.NguyenLieuId);
                    listCTDetails.Add(new Tuple<NguyenLieu, int>(nguyenLieu, (int)item.SoLuong));
                }
            }    
            return PartialView("_PartialViewShowCongThuc", listCTDetails);
        }
        public IActionResult ShowNguyenLieu()
        {

            var listNguyenLieu= _context.NguyenLieus.ToList();
            return PartialView("_PartialViewShowNguyenLieu", listNguyenLieu);
        }
        [HttpPost]
        public IActionResult HoanThanhDonHang(int monId)
        {
            
            var mon = _context.Mons.Find(monId);
            if (mon!=null)
            {
                var ListCT = _context.CongThucNguyenLieus.Where(x => x.CongThucId==mon.CongThucId).ToList();

                foreach (var nguyenLieu in ListCT)
                {
                    var nguyenLieuTrongKho = _context.NguyenLieus
                        .FirstOrDefault(x => x.NguyenLieuId == nguyenLieu.NguyenLieuId);
                    if (nguyenLieuTrongKho != null)
                    {
                        nguyenLieuTrongKho.SoLuong -= nguyenLieu.SoLuong;
                    }
                }
                _context.SaveChanges();

            }
            // Trả về dữ liệu số lượng nguyên liệu còn lại trong kho
            var soLuongNguyenLieuConLai = _context.NguyenLieus.Select(x => x.SoLuong);
            return Json(new { soLuongNguyenLieuConLai });
        }
        public IActionResult HuyDonHang(int monId)
        {
            try
            {
                // Tìm kiếm đơn hàng tương ứng từ cơ sở dữ liệu
                //var donhang = _context.DonHangs.FirstOrDefault(x => x.Id == id);

                //// Nếu không tìm thấy đơn hàng, trả về kết quả lỗi
                //if (donhang == null)
                //    return Json(new { success = false, message = "Không tìm thấy đơn hàng." });

                //// Cập nhật trạng thái của đơn hàng thành "Đã hủy"
                //donhang.TrangThai = "DaHuy";
                //_context.SaveChanges();

                // Trả về kết quả thành công
                return Json(new { success = true, message = "Hủy đơn hàng thành công!" });
            }
            catch (Exception ex)
            {
                // Nếu xảy ra lỗi, trả về kết quả lỗi
                return Json(new { success = false, message = "Lỗi khi hủy đơn hàng: " + ex.Message });
            }
        }

    }
}
