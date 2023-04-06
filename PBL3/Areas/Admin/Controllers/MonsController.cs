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
    public class MonsController : Controller
    {
        private readonly TamtentoiContext _context;

        public MonsController(TamtentoiContext context)
        {
            _context = context;
        }

        // GET: Admin/Mons
        public async Task<IActionResult> Index()
        {
            var tamtentoiContext = _context.Mons.Include(m => m.CongThuc).Include(m => m.LoaiMon);
            return View(await tamtentoiContext.ToListAsync());
        }

        // GET: Admin/Mons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mons == null)
            {
                return NotFound();
            }

            var mon = await _context.Mons
                .Include(m => m.CongThuc)
                .Include(m => m.LoaiMon)
                .FirstOrDefaultAsync(m => m.MonId == id);
            if (mon == null)
            {
                return NotFound();
            }

            return View(mon);
        }

        // GET: Admin/Mons/Create
        public IActionResult Create()
        {
            ViewData["CongThucId"] = new SelectList(_context.CongThucs, "CongThucId", "CongThucId");
            ViewData["LoaiMonId"] = new SelectList(_context.LoaiMons, "LoaiMonId", "LoaiMonId");
            return View();
        }

        // POST: Admin/Mons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MonId,TenMon,Gia,LoaiMonId,CongThucId")] Mon mon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CongThucId"] = new SelectList(_context.CongThucs, "CongThucId", "CongThucId", mon.CongThucId);
            ViewData["LoaiMonId"] = new SelectList(_context.LoaiMons, "LoaiMonId", "LoaiMonId", mon.LoaiMonId);
            return View(mon);
        }

        // GET: Admin/Mons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mons == null)
            {
                return NotFound();
            }

            var mon = await _context.Mons.FindAsync(id);
            if (mon == null)
            {
                return NotFound();
            }
            ViewData["CongThucId"] = new SelectList(_context.CongThucs, "CongThucId", "CongThucId", mon.CongThucId);
            ViewData["LoaiMonId"] = new SelectList(_context.LoaiMons, "LoaiMonId", "LoaiMonId", mon.LoaiMonId);
            return View(mon);
        }

        // POST: Admin/Mons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MonId,TenMon,Gia,LoaiMonId,CongThucId")] Mon mon)
        {
            if (id != mon.MonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonExists(mon.MonId))
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
            ViewData["CongThucId"] = new SelectList(_context.CongThucs, "CongThucId", "CongThucId", mon.CongThucId);
            ViewData["LoaiMonId"] = new SelectList(_context.LoaiMons, "LoaiMonId", "LoaiMonId", mon.LoaiMonId);
            return View(mon);
        }

        // GET: Admin/Mons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mons == null)
            {
                return NotFound();
            }

            var mon = await _context.Mons
                .Include(m => m.CongThuc)
                .Include(m => m.LoaiMon)
                .FirstOrDefaultAsync(m => m.MonId == id);
            if (mon == null)
            {
                return NotFound();
            }

            return View(mon);
        }

        // POST: Admin/Mons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mons == null)
            {
                return Problem("Entity set 'TamtentoiContext.Mons'  is null.");
            }
            var mon = await _context.Mons.FindAsync(id);
            if (mon != null)
            {
                _context.Mons.Remove(mon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonExists(int id)
        {
          return (_context.Mons?.Any(e => e.MonId == id)).GetValueOrDefault();
        }
    }
}
