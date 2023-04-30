using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using PBL3.Models;

namespace PBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DatHangsController : Controller
    {
        private readonly TamtentoiContext _context;
		public INotyfService _notifyService { get; }
		public static List<Tuple<NguyenLieu, int>> listNguyenLieu = new List<Tuple<NguyenLieu, int>>();

		public DatHangsController(TamtentoiContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        // GET: Admin/DatHangs
		public async Task<IActionResult> Index(int? page, string? search)
		{
            ResetListItem();
			var pageNumber = page == null || page < 0 ? 1 : page.Value;
			var pageSize = 10;
			ViewData["CurrentSearch"] = search;
			var list = _context.DatHangs.Include(n => n.NhanVien)
										   .OrderByDescending(n => n.DathangId).ToList();
			if (!string.IsNullOrEmpty(search))
			{
				ViewData["OrderTitle"] = "Danh sách đơn đặt hàng theo ngày chỉ định";
				var date = search.Split("-");
				list = list.Where(x => x.NgayDat.Value.Year == Convert.ToInt32(date[0])
								   && x.NgayDat.Value.Month == Convert.ToInt32(date[1])
								   && x.NgayDat.Value.Day == Convert.ToInt32(date[2])).ToList();
			}
			else
			{
				ViewData["OrderTitle"] = "Danh sách đơn đặt hàng hôm nay";
				var dateTime = DateTime.Today;
				string formattedDateTime = dateTime.ToString("MM/dd/yyyy");
				list = list.Where(x => x.NgayDat.Value.ToString().Contains(formattedDateTime)).ToList();
			}
			PagedList<DatHang> models = new PagedList<DatHang>(list.AsQueryable(), pageNumber, pageSize);
			return View(models);
		}

		// GET: Admin/DatHangs/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DatHangs == null)
            {
                return NotFound();
            }

            var datHang = await _context.DatHangs
                .Include(d => d.NhanVien)
                .FirstOrDefaultAsync(m => m.DathangId == id);
            if (datHang == null)
            {
                return NotFound();
            }
            ViewBag.datHang= datHang;
            var listOrderDetail = _context.DatHangNguyenLieus.Where(x=>x.DathangId ==id).ToList();
            ViewBag.ListOrderDetail = listOrderDetail;
			var list = new List<Tuple<NguyenLieu, int>>();
            foreach(var item in listOrderDetail)
            {
				var nguyenLieu = _context.NguyenLieus.Find(item.NguyenLieuId);
				if (nguyenLieu != null)
				{
					list.Add(new Tuple<NguyenLieu, int>(nguyenLieu, (int)item.SoLuong));

				}
			}
			return View(list);
        }

        // GET: Admin/DatHangs/Create
        public IActionResult Create()
        {
            var listNguyenLieuDaDat = listNguyenLieu.Select(x=>x.Item1.NguyenLieuId).ToList();

			ViewBag.NguyenLieuChuaDat = _context.NguyenLieus.Where(x=> !listNguyenLieuDaDat.Contains(x.NguyenLieuId))
                                                            .ToList();
            return View(listNguyenLieu);
        }
		[HttpPost]
		public IActionResult AddNguyenLieu(int nguyenLieuId, string note ="", int soLuong = 1)
        {
            var nguyenLieu = _context.NguyenLieus.Find(nguyenLieuId);
			if (nguyenLieu != null)
            {
                listNguyenLieu.Add(new Tuple<NguyenLieu, int>(nguyenLieu, soLuong));
				_notifyService.Success("Thêm thành công!");

			}
			TempData["Note"] = note;
			return RedirectToAction("Create");

		}
        public IActionResult DeleteNguyenLieu(int nguyenLieuId, string note="")
        {
            
            var nguyenLieuRemoved = listNguyenLieu.Where(x => x.Item1.NguyenLieuId == nguyenLieuId).SingleOrDefault();
            listNguyenLieu.Remove(nguyenLieuRemoved);
			TempData["Note"] = note;
			_notifyService.Success("Xóa thành công!");
			return RedirectToAction("Create");
		}

        // POST: Admin/DatHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DathangId,NgayDat,GhiChu,NhanVienId")] DatHang datHang)
        {
			TempData["Note"] = datHang.GhiChu;
			if (ModelState.IsValid)
            {
                if (listNguyenLieu.Count > 0)
                {
					datHang.NgayDat = DateTime.Now;
					datHang.NhanVienId = HttpContext.Session.GetInt32("user");
					_context.Add(datHang);
					await _context.SaveChangesAsync();

                    var orderId = _context.DatHangs.ToList().MaxBy(x => x.DathangId).DathangId;
                    List<DatHangNguyenLieu> listOrderDetail = new List<DatHangNguyenLieu>();
                    foreach(var item in listNguyenLieu)
                    {
						var orderDetail = new DatHangNguyenLieu();
                        orderDetail.NguyenLieuId = item.Item1.NguyenLieuId;
                        orderDetail.DathangId = orderId;
                        orderDetail.SoLuong = item.Item2;
                        orderDetail.TongGia = _context.NguyenLieus.Find(item.Item1.NguyenLieuId).Gia * item.Item2;

                        listOrderDetail.Add(orderDetail);

					}
                    _context.AddRange(listOrderDetail.ToArray());
                    _context.SaveChanges();
                    ResetListItem();
                    TempData["Note"] = "";
					_notifyService.Success("Thêm thành công!");
					return RedirectToAction(nameof(Index));
				}
                else
                {
					TempData["Empty"] = "Vui lòng thêm nguyên liệu";
					return RedirectToAction("Create");
				}
                
            }
			
			return RedirectToAction("Create");
        }

        // GET: Admin/DatHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DatHangs == null)
            {
                return NotFound();
            }

            var datHang = await _context.DatHangs.FindAsync(id);
            if (datHang == null)
            {
                return NotFound();
            }
            ViewData["NhanVienId"] = new SelectList(_context.NhanViens, "NhanVienId", "NhanVienId", datHang.NhanVienId);
            return View(datHang);
        }

        // POST: Admin/DatHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DathangId,NgayDat,GhiChu,NhanVienId")] DatHang datHang)
        {
            if (id != datHang.DathangId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatHangExists(datHang.DathangId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NhanVienId"] = new SelectList(_context.NhanViens, "NhanVienId", "NhanVienId", datHang.NhanVienId);
            return View(datHang);
        }

        // GET: Admin/DatHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
			if (id == null || _context.DatHangs == null)
			{
				return NotFound();
			}

			var datHang = await _context.DatHangs
				.Include(d => d.NhanVien)
				.FirstOrDefaultAsync(m => m.DathangId == id);
			if (datHang == null)
			{
				return NotFound();
			}
			ViewBag.datHang = datHang;
			var listOrderDetail = _context.DatHangNguyenLieus.Where(x => x.DathangId == id).ToList();
			ViewBag.ListOrderDetail = listOrderDetail;
			var list = new List<Tuple<NguyenLieu, int>>();
			foreach (var item in listOrderDetail)
			{
				var nguyenLieu = _context.NguyenLieus.Find(item.NguyenLieuId);
				if (nguyenLieu != null)
				{
					list.Add(new Tuple<NguyenLieu, int>(nguyenLieu, (int)item.SoLuong));

				}
			}
			return View(list);
		}

        // POST: Admin/DatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DatHangs == null)
            {
                return Problem("Entity set 'TamtentoiContext.DatHangs'  is null.");
            }
            var datHang = await _context.DatHangs.FindAsync(id);
            if (datHang != null)
            {
                _context.DatHangs.Remove(datHang);
            }
            
            await _context.SaveChangesAsync();
			_notifyService.Success("Xóa thành công!");
			return RedirectToAction(nameof(Index));
        }

        private bool DatHangExists(int id)
        {
          return (_context.DatHangs?.Any(e => e.DathangId == id)).GetValueOrDefault();
        }
		public void ResetListItem()
		{
            listNguyenLieu.Clear();
		}
        public IActionResult ConfirmOrder(int id)
        {
            var datHang = _context.DatHangs.Find(id);
            if(datHang != null)
            {
                datHang.TinhTrangXacNhan = true;
            }
            var listOrder = _context.DatHangNguyenLieus.Where(x=>x.DathangId ==id).ToList();    
            foreach(var item in listOrder)
            {
                var nguyenLieu = _context.NguyenLieus.Find(item.NguyenLieuId);
                if(nguyenLieu!= null)
                {
                    nguyenLieu.SoLuong += item.SoLuong;
					_context.SaveChanges();
				}
			}
			_notifyService.Success("Xác nhận thành công!");
			return RedirectToAction("Details", new { id = id });
		    
		}

	}
}
