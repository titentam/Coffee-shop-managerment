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
    public class LoaiNhanViensController : Controller
    {
        private readonly TamtentoiContext _context;
		public INotyfService _notifyService { get; }

		public LoaiNhanViensController(TamtentoiContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService; 
        }

        // GET: Admin/LoaiNhanViens
        public IActionResult Index(string? search)
        {
            ViewData["CurrentSearch"] = search;
            var list = _context.LoaiNhanViens.OrderByDescending(n => n.LoaiNv).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                list = list.Where(nv => nv.TenLoai.Contains(search)).ToList();
            }
            return View(list);
        }
        // GET: Admin/LoaiNhanViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoaiNhanViens == null)
            {
                return NotFound();
            }

            var loaiNhanVien = await _context.LoaiNhanViens
                .FirstOrDefaultAsync(m => m.LoaiNv == id);
            if (loaiNhanVien == null)
            {
                return NotFound();
            }

            return View(loaiNhanVien);
        }

        // GET: Admin/LoaiNhanViens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiNhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiNv,TenLoai")] LoaiNhanVien loaiNhanVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiNhanVien);
                await _context.SaveChangesAsync();
				_notifyService.Success("Thêm thành công!");
				return RedirectToAction(nameof(Index));
            }
            return View(loaiNhanVien);
        }

        // GET: Admin/LoaiNhanViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoaiNhanViens == null)
            {
                return NotFound();
            }

            var loaiNhanVien = await _context.LoaiNhanViens.FindAsync(id);
            if (loaiNhanVien == null)
            {
                return NotFound();
            }
            return View(loaiNhanVien);
        }

        // POST: Admin/LoaiNhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoaiNv,TenLoai")] LoaiNhanVien loaiNhanVien)
        {
            if (id != loaiNhanVien.LoaiNv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiNhanVien);
                    await _context.SaveChangesAsync();
					_notifyService.Success("Cập nhật thành công!");
				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiNhanVienExists(loaiNhanVien.LoaiNv))
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
            return View(loaiNhanVien);
        }

        // GET: Admin/LoaiNhanViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoaiNhanViens == null)
            {
                return NotFound();
            }

            var loaiNhanVien = await _context.LoaiNhanViens
                .FirstOrDefaultAsync(m => m.LoaiNv == id);
            if (loaiNhanVien == null)
            {
                return NotFound();
            }

            return View(loaiNhanVien);
        }

        // POST: Admin/LoaiNhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoaiNhanViens == null)
            {
                return Problem("Entity set 'TamtentoiContext.LoaiNhanViens'  is null.");
            }
            var loaiNhanVien = await _context.LoaiNhanViens.FindAsync(id);
            if (loaiNhanVien != null)
            {
                _context.LoaiNhanViens.Remove(loaiNhanVien);
            }
            
            await _context.SaveChangesAsync();
			_notifyService.Success("Xoá thành công!");
			return RedirectToAction(nameof(Index));
        }

        private bool LoaiNhanVienExists(int id)
        {
          return (_context.LoaiNhanViens?.Any(e => e.LoaiNv == id)).GetValueOrDefault();
        }
    }
}
