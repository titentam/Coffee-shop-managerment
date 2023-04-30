using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
    public class CongThucsController : Controller
    {
        private readonly TamtentoiContext _context;
		public INotyfService _notifyService { get; }

		public CongThucsController(TamtentoiContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        // GET: Admin/CongThucs
		public async Task<IActionResult> Index(int? page, string? search)
		{
			var pageNumber = page == null || page < 0 ? 1 : page.Value;
			var pageSize = 10;
			ViewData["CurrentSearch"] = search;
			var list = _context.CongThucs.OrderByDescending(n => n.CongThucId).ToList();
			if (!string.IsNullOrEmpty(search))
			{
				list = list.Where(nv => nv.TenCongThuc.Contains(search)).ToList();
			}
			PagedList<CongThuc> models = new PagedList<CongThuc>(list.AsQueryable(), pageNumber, pageSize);
			return View(models);
		}

		// GET: Admin/CongThucs/Details/5
		public async Task<IActionResult> Details(int? id, string? search, int? page)
		{
			var pageNumber = page == null || page < 0 ? 1 : page.Value;
			var pageSize = 10;
			ViewData["CurrentSearch"] = search;
			if (id == null || _context.CongThucs == null)
			{
				return NotFound();
			}

			var congThuc = await _context.CongThucs
				.FirstOrDefaultAsync(m => m.CongThucId == id);
			if (congThuc == null)
			{
				return NotFound();
			}

			var listCongThuc = _context.CongThucNguyenLieus.Include(x=>x.NguyenLieu)
                .Where(x=>x.CongThucId == id).ToList();
			if (!string.IsNullOrEmpty(search))
			{
				listCongThuc = listCongThuc.Where(x => x.NguyenLieu.TenNguyenLieu.Contains(search)).ToList();
			}

			ViewBag.congThuc = congThuc;
			PagedList<CongThucNguyenLieu> models = new PagedList<CongThucNguyenLieu>(listCongThuc.AsQueryable(), pageNumber, pageSize);
			return View(models);
		}

		// GET: Admin/CongThucs/Create
		public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CongThucs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CongThucId,TenCongThuc")] CongThuc congThuc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(congThuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(congThuc);
        }

        // GET: Admin/CongThucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CongThucs == null)
            {
                return NotFound();
            }

            var congThuc = await _context.CongThucs.FindAsync(id);
            if (congThuc == null)
            {
                return NotFound();
            }
            return View(congThuc);
        }

        // POST: Admin/CongThucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CongThucId,TenCongThuc")] CongThuc congThuc)
        {
            if (id != congThuc.CongThucId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(congThuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CongThucExists(congThuc.CongThucId))
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
            return View(congThuc);
        }

        // GET: Admin/CongThucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CongThucs == null)
            {
                return NotFound();
            }

            var congThuc = await _context.CongThucs
                .FirstOrDefaultAsync(m => m.CongThucId == id);
            if (congThuc == null)
            {
                return NotFound();
            }

            return View(congThuc);
        }

        // POST: Admin/CongThucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CongThucs == null)
            {
                return Problem("Entity set 'TamtentoiContext.CongThucs'  is null.");
            }
            var congThuc = await _context.CongThucs.FindAsync(id);
            if (congThuc != null)
            {
                _context.CongThucs.Remove(congThuc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CongThucExists(int id)
        {
          return (_context.CongThucs?.Any(e => e.CongThucId == id)).GetValueOrDefault();
        }

		public IActionResult AddNguyenLieu(int id)
		{
			ViewBag.id = id;
            var listAvailable = _context.CongThucNguyenLieus.Where(x=>x.CongThucId == id).Select(x=>x.NguyenLieuId).ToList();
			var listNguyenLieu = _context.NguyenLieus.Include(x => x.NhaCungCap)
                .Where(x=> !listAvailable.Contains(x.NguyenLieuId))
                .ToList();
			return View(listNguyenLieu);
		}

		[HttpPost]
		public IActionResult AddNguyenLieu(int id, int nguyenLieuId, int soLuong)
		{
            var ctNl = new CongThucNguyenLieu();
            ctNl.CongThucId = id;
            ctNl.NguyenLieuId = nguyenLieuId;
            ctNl.SoLuong = soLuong;
            _context.Add(ctNl);
			_context.SaveChanges();
			_notifyService.Success("Thêm nguyên liệu thành công!");
			
			return RedirectToAction("AddNguyenLieu", new { id = id });
		}
		[HttpPost]
		public IActionResult DeleteNguyenLieu(int congThucId, int nguyenLieuId)
		{
            var ctNl = _context.CongThucNguyenLieus.Where(x => x.NguyenLieuId == nguyenLieuId && x.CongThucId == congThucId)
                .SingleOrDefault();
			_context.Remove(ctNl);
			_context.SaveChanges();
			_notifyService.Success("Xóa nguyên liệu thành công!");

			return RedirectToAction("Details", new { id = congThucId });
		}
		[HttpGet]
		public IActionResult EditNguyenLieu([FromQuery] int congThucId, int nguyenLieuId)
		{
            var ctNl = _context.CongThucNguyenLieus.Where(x => x.NguyenLieuId == nguyenLieuId && x.CongThucId == congThucId)
                .SingleOrDefault();
            ViewBag.ctNl = ctNl;
            return View(_context.NguyenLieus.Find(nguyenLieuId));
		}
        [HttpPost]
        public IActionResult EditNguyenLieu(int congThucId, int nguyenLieuId, int soLuong)
        {
            var ctNl = _context.CongThucNguyenLieus.Where(x => x.NguyenLieuId == nguyenLieuId && x.CongThucId == congThucId)
                 .SingleOrDefault();
            ctNl.SoLuong = soLuong;
            _context.SaveChanges();
            _notifyService.Success("Điều chỉnh công thức thành công!");
            return RedirectToAction("Details", new { id = congThucId });
        }

    }
}
