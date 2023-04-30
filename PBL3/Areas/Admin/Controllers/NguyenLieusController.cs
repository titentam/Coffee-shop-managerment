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
    public class NguyenLieusController : Controller
    {
        private readonly TamtentoiContext _context;
        public INotyfService _notifyService { get; }

        public NguyenLieusController(TamtentoiContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        // GET: Admin/NguyenLieus
        public async Task<IActionResult> Index(int? page, string? search, int option = 0)
        {
            var pageNumber = page == null || page < 0 ? 1 : page.Value;
            var pageSize = 10;
            ViewData["CurrentSearch"] = search;
            ViewData["DsNhaCungCap"] = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap", option);
            var list = _context.NguyenLieus.Include(n => n.NhaCungCap)
                                           .OrderByDescending(n => n.NguyenLieuId).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                list = list.Where(nv => nv.TenNguyenLieu.Contains(search)).ToList();
            }
            if (option != 0)
            {
                list = list.Where(nv => nv.NhaCungCapId == option).ToList();
            }
            PagedList<NguyenLieu> models = new PagedList<NguyenLieu>(list.AsQueryable(), pageNumber, pageSize);
            return View(models);
        }

        // GET: Admin/NguyenLieus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NguyenLieus == null)
            {
                return NotFound();
            }

            var nguyenLieu = await _context.NguyenLieus
                .Include(n => n.NhaCungCap)
                .FirstOrDefaultAsync(m => m.NguyenLieuId == id);
            if (nguyenLieu == null)
            {
                return NotFound();
            }

            return View(nguyenLieu);
        }

        // GET: Admin/NguyenLieus/Create
        public IActionResult Create()
        {
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap");
            ViewData["DonViTinhh"] = new SelectList(new List<SelectListItem>{
                new SelectListItem { Text = "g", Value = "g"},
                new SelectListItem { Text = "ml", Value = "ml"},
                new SelectListItem { Text = "cái", Value = "cái"},
                new SelectListItem { Text = "chai", Value = "chai"},
                }, "Value", "Text");
            return View();
        }

        // POST: Admin/NguyenLieus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NguyenLieuId,TenNguyenLieu,SoLuong,Gia,DonViTinh,NhaCungCapId")] NguyenLieu nguyenLieu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nguyenLieu);
                await _context.SaveChangesAsync();
                _notifyService.Success("Thêm thành công!");
                return RedirectToAction(nameof(Index));
            }
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap", nguyenLieu.NhaCungCapId);
            return View(nguyenLieu);
        }

        // GET: Admin/NguyenLieus/Edit/5
        public async Task<IActionResult> Edit(int? id, string donvitinh = "g")
        {
            if (id == null || _context.NguyenLieus == null)
            {
                return NotFound();
            }

            var nguyenLieu = await _context.NguyenLieus.FindAsync(id);
            if (nguyenLieu == null)
            {
                return NotFound();
            }
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap", nguyenLieu.NhaCungCapId);
            ViewData["DonViTinhh"] = new SelectList(new List<SelectListItem>{
                new SelectListItem { Text = "g", Value = "g"},
                new SelectListItem { Text = "ml", Value = "ml"},
                new SelectListItem { Text = "cái", Value = "cái"},
                new SelectListItem { Text = "chai", Value = "chai"},
                }, "Value", "Text", donvitinh);
            return View(nguyenLieu);
        }

        // POST: Admin/NguyenLieus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NguyenLieuId,TenNguyenLieu,SoLuong,Gia,DonViTinh,NhaCungCapId")] NguyenLieu nguyenLieu, string donvitinh = "g")
        {
            if (id != nguyenLieu.NguyenLieuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nguyenLieu);
                    _notifyService.Success("Chỉnh sửa thành công!");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguyenLieuExists(nguyenLieu.NguyenLieuId))
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
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap", nguyenLieu.NhaCungCapId);
            ViewData["DonViTinhh"] = new SelectList(new List<SelectListItem>{
                new SelectListItem { Text = "g", Value = "g"},
                new SelectListItem { Text = "ml", Value = "ml"},
                new SelectListItem { Text = "cái", Value = "cái"},
                new SelectListItem { Text = "chai", Value = "chai"},
                }, "Value", "Text", donvitinh);
            return View(nguyenLieu);
        }

        // GET: Admin/NguyenLieus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NguyenLieus == null)
            {
                return NotFound();
            }

            var nguyenLieu = await _context.NguyenLieus
                .Include(n => n.NhaCungCap)
                .FirstOrDefaultAsync(m => m.NguyenLieuId == id);
            if (nguyenLieu == null)
            {
                return NotFound();
            }

            return View(nguyenLieu);
        }

        // POST: Admin/NguyenLieus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NguyenLieus == null)
            {
                return Problem("Entity set 'TamtentoiContext.NguyenLieus'  is null.");
            }
            var nguyenLieu = await _context.NguyenLieus.FindAsync(id);
            if (nguyenLieu != null)
            {
                _context.NguyenLieus.Remove(nguyenLieu);
            }

            await _context.SaveChangesAsync();
            _notifyService.Success("Xóa thành công!");
            return RedirectToAction(nameof(Index));
        }

        private bool NguyenLieuExists(int id)
        {
            return (_context.NguyenLieus?.Any(e => e.NguyenLieuId == id)).GetValueOrDefault();
        }
    }
}
