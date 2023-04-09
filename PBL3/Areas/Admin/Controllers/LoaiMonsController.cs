using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PBL3.Models;

namespace PBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiMonsController : Controller
    {
        private readonly TamtentoiContext _context;
		public INotyfService _notifyService { get; }

		public LoaiMonsController(TamtentoiContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

		// GET: Admin/LoaiMons
		public IActionResult Index(string? search)
		{
			ViewData["CurrentSearch"] = search;
			var list = _context.LoaiMons.OrderByDescending(n => n.LoaiMonId).ToList();
			if (!string.IsNullOrEmpty(search))
			{
				list = list.Where(nv => nv.TenLoaiMon.Contains(search)).ToList();
			}
			return View(list);
		}

		// GET: Admin/LoaiMons/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoaiMons == null)
            {
                return NotFound();
            }

            var loaiMon = await _context.LoaiMons
                .FirstOrDefaultAsync(m => m.LoaiMonId == id);
            if (loaiMon == null)
            {
                return NotFound();
            }

            return View(loaiMon);
        }

        // GET: Admin/LoaiMons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiMons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiMonId,TenLoaiMon")] LoaiMon loaiMon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiMon);
                await _context.SaveChangesAsync();
                _notifyService.Success("Thêm thành công!");
                return RedirectToAction(nameof(Index));
            }
            return View(loaiMon);
        }

        // GET: Admin/LoaiMons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoaiMons == null)
            {
                return NotFound();
            }

            var loaiMon = await _context.LoaiMons.FindAsync(id);
            if (loaiMon == null)
            {
                return NotFound();
            }
            return View(loaiMon);
        }

        // POST: Admin/LoaiMons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoaiMonId,TenLoaiMon")] LoaiMon loaiMon)
        {
            if (id != loaiMon.LoaiMonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiMon);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiMonExists(loaiMon.LoaiMonId))
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
            return View(loaiMon);
        }

        // GET: Admin/LoaiMons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoaiMons == null)
            {
                return NotFound();
            }

            var loaiMon = await _context.LoaiMons
                .FirstOrDefaultAsync(m => m.LoaiMonId == id);
            if (loaiMon == null)
            {
                return NotFound();
            }

            return View(loaiMon);
        }

        // POST: Admin/LoaiMons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoaiMons == null)
            {
                return Problem("Entity set 'TamtentoiContext.LoaiMons'  is null.");
            }
            var loaiMon = await _context.LoaiMons.FindAsync(id);
            if (loaiMon != null)
            {
                _context.LoaiMons.Remove(loaiMon);
            }
            
            await _context.SaveChangesAsync();
            _notifyService.Success("Xoá thành công!");
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiMonExists(int id)
        {
          return (_context.LoaiMons?.Any(e => e.LoaiMonId == id)).GetValueOrDefault();
        }
    }
}
