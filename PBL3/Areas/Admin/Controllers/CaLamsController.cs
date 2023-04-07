using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PBL3.Models;

namespace PBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CaLamsController : Controller
    {
        private readonly TamtentoiContext _context;

        public CaLamsController(TamtentoiContext context)
        {
            _context = context;
        }

        // GET: Admin/CaLams
        public async Task<IActionResult> Index()
        {
              return _context.CaLams != null ? 
                          View(await _context.CaLams
                          .Include(ca=>ca.NhanViens)
                          .ToListAsync()) :
                          Problem("Entity set 'TamtentoiContext.CaLams'  is null.");
        }

        // GET: Admin/CaLams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CaLams == null)
            {
                return NotFound();
            }

            var caLam = await _context.CaLams
                .FirstOrDefaultAsync(m => m.CaId == id);
            if (caLam == null)
            {
                return NotFound();
            }

            return View(caLam);
        }

        // GET: Admin/CaLams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CaLams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CaId,TenCa,ThoiGianBatDau,ThoiGianKetThuc")] CaLam caLam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caLam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caLam);
        }

        // GET: Admin/CaLams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CaLams == null)
            {
                return NotFound();
            }

            var caLam = await _context.CaLams.FindAsync(id);
            if (caLam == null)
            {
                return NotFound();
            }
            return View(caLam);
        }

        // POST: Admin/CaLams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaId,TenCa,ThoiGianBatDau,ThoiGianKetThuc")] CaLam caLam)
        {
            if (id != caLam.CaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caLam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaLamExists(caLam.CaId))
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
            return View(caLam);
        }

        // GET: Admin/CaLams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CaLams == null)
            {
                return NotFound();
            }

            var caLam = await _context.CaLams
                .FirstOrDefaultAsync(m => m.CaId == id);
            if (caLam == null)
            {
                return NotFound();
            }

            return View(caLam);
        }

        // POST: Admin/CaLams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CaLams == null)
            {
                return Problem("Entity set 'TamtentoiContext.CaLams'  is null.");
            }
            var caLam = await _context.CaLams.FindAsync(id);
            if (caLam != null)
            {
                _context.CaLams.Remove(caLam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaLamExists(int id)
        {
          return (_context.CaLams?.Any(e => e.CaId == id)).GetValueOrDefault();
        }
    }
}
