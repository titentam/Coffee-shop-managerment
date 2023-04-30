using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.WebSockets;
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
    public class HoaDonsController : Controller
    {
        private readonly TamtentoiContext _context;
        public INotyfService _notifyService { get; }

        public HoaDonsController(TamtentoiContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        // GET: Admin/HoaDons
		public async Task<IActionResult> Index(int? page, string? search)
		{
			var pageNumber = page == null || page < 0 ? 1 : page.Value;
			var pageSize = 10;
			ViewData["CurrentSearch"] = search;
			var list = _context.HoaDons.Include(n => n.NhanVien)
										   .OrderByDescending(n => n.HoaDonId).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                ViewData["InvoiceTitle"] = "Danh sách hóa đơn theo ngày chỉ định";
                var date = search.Split("-");
                list = list.Where(x=> x.ThoiGianTao.Value.Year == Convert.ToInt32(date[0])
                                   && x.ThoiGianTao.Value.Month == Convert.ToInt32(date[1])
                                   && x.ThoiGianTao.Value.Day == Convert.ToInt32(date[2])).ToList(); 
            }
            else
            {
                ViewData["InvoiceTitle"] = "Danh sách hóa đơn hôm nay";
                var dateTime = DateTime.Today;
				string formattedDateTime = dateTime.ToString("MM/dd/yyyy");
                list = list.Where(x=>x.ThoiGianTao.Value.ToString().Contains(formattedDateTime)).ToList();
			}
			PagedList<HoaDon> models = new PagedList<HoaDon>(list.AsQueryable(), pageNumber, pageSize);
			return View(models);
		}

		// GET: Admin/HoaDons/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoaDons == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.NhanVien)
                .FirstOrDefaultAsync(m => m.HoaDonId == id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            ViewBag.hoaDon = hoaDon;
            List<Tuple<Mon,int>> listItem = new List<Tuple<Mon,int>>(); 
            var order = _context.DonDatMons.Where(x => x.HoaDonId == id).SingleOrDefault();
            if(order!= null)
            {
                var orderDetails = _context.MonDonDatMons.Where(x => x.DonDatMonId == order.DonDatMonId).ToList();
                foreach(var orderDetail in orderDetails)
                {
                    var item = _context.Mons.Find(orderDetail.MonId);
                    listItem.Add(new Tuple<Mon,int>(item, (int)orderDetail.SoLuong));
                }
            }

            return View(listItem);
        }

        // GET: Admin/HoaDons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoaDons == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            ViewData["NhanVienId"] = new SelectList(_context.NhanViens, "NhanVienId", "NhanVienId", hoaDon.NhanVienId);
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HoaDonId,ThoiGianTao,TongTien,TrangThaiThanhToan,NhanVienId")] HoaDon hoaDon)
        {
            if (id != hoaDon.HoaDonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công!");

				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.HoaDonId))
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
            ViewData["NhanVienId"] = new SelectList(_context.NhanViens, "NhanVienId", "NhanVienId", hoaDon.NhanVienId);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoaDons == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.NhanVien)
                .FirstOrDefaultAsync(m => m.HoaDonId == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoaDons == null)
            {
                return Problem("Entity set 'TamtentoiContext.HoaDons'  is null.");
            }
            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon != null)
            {
                _context.HoaDons.Remove(hoaDon);
            }
            
            await _context.SaveChangesAsync();
			_notifyService.Success("Xóa thành công!");
			return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(int id)
        {
          return (_context.HoaDons?.Any(e => e.HoaDonId == id)).GetValueOrDefault();
        }

        [HttpPost]
        public IActionResult DeleteHoaDon(int hoaDonId, int monId)
        {
            var order = _context.DonDatMons.Where(x => x.HoaDonId == hoaDonId).SingleOrDefault();
            if (order != null)
            {
                var item = _context.MonDonDatMons.Where(x=>x.DonDatMonId == order.DonDatMonId&& x.MonId== monId).SingleOrDefault();
                if (item != null)
                {
					// update total price in invoice
					var invoice = _context.HoaDons.Find(hoaDonId);
                    var mon = _context.Mons.Find(monId);
                    invoice.TongTien -= (mon.Gia ?? 0) * (int)item.SoLuong;
                    
					_context.Remove(item);
					_context.SaveChanges();
					_notifyService.Success("Xóa món thành công!");
				}
            }
            return RedirectToAction("Details", new { id = hoaDonId });
        }
		[HttpGet]
		public IActionResult EditHoaDon([FromQuery] int hoaDonId, int monId)
		{
			ViewBag.hoaDonId = hoaDonId;
            var order = _context.DonDatMons.Where(x => x.HoaDonId == hoaDonId).SingleOrDefault();
            if (order != null)
            {
                var item = _context.MonDonDatMons.Where(x => x.DonDatMonId == order.DonDatMonId && x.MonId == monId).SingleOrDefault();
                if (item != null)
                {
                    ViewBag.soLuong = item.SoLuong;
                }
            }
            return View(_context.Mons.Find(monId));
		}
		[HttpPost]
		public IActionResult EditHoaDon(int hoaDonId, int monId, int soLuong)
		{
            var order = _context.DonDatMons.Where(x => x.HoaDonId == hoaDonId).SingleOrDefault();
            if (order != null)
            {
                var item = _context.MonDonDatMons.Where(x => x.DonDatMonId == order.DonDatMonId && x.MonId == monId).SingleOrDefault();
                if (item != null)
                {
                    // update total price in invoice
                    var invoice = _context.HoaDons.Find(hoaDonId);
                    var mon = _context.Mons.Find(monId);
                    invoice.TongTien -= (mon.Gia ?? 0) * (int)item.SoLuong;

                    item.SoLuong = soLuong;
                    invoice.TongTien += (mon.Gia ?? 0) * (int)item.SoLuong;
                    _context.SaveChanges();
                    _notifyService.Success("Cập nhật thành công!");
                }
            }
			return RedirectToAction("Details", new { id = hoaDonId });
		}
        
	}
}
