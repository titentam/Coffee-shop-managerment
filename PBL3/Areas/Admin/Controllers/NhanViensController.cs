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
    public class NhanViensController : Controller
    {
        private readonly TamtentoiContext _context;

        public NhanViensController(TamtentoiContext context)
        {
            _context = context;
        }

        // GET: Admin/NhanViens
        public async Task<IActionResult> Index()
        {
            var tamtentoiContext = _context.NhanViens.Include(n => n.Ca).Include(n => n.LoaiNvNavigation).Include(n => n.NhanVienQlNavigation);
            return View(await tamtentoiContext.ToListAsync());
        }

        // GET: Admin/NhanViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.Ca)
                .Include(n => n.LoaiNvNavigation)
                .Include(n => n.NhanVienQlNavigation)
                .FirstOrDefaultAsync(m => m.NhanVienId == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Create
        public IActionResult Create()
        {
            ViewData["CaId"] = new SelectList(_context.CaLams, "CaId", "TenCa");
            ViewData["LoaiNv"] = new SelectList(_context.LoaiNhanViens, "LoaiNv", "TenLoai");
            var nvQl = _context.NhanViens.Include(nv => nv.LoaiNvNavigation)
                                         .Where(nv => nv.LoaiNvNavigation.TenLoai.Contains("Quản lý"));
            ViewData["NhanVienQl"] = new SelectList(nvQl, "NhanVienId", "TenNhanVien");

            return View();
        }

        // POST: Admin/NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NhanVienId,TenNhanVien,Sdt,DiaChi,LoaiNv,NhanVienQl,CaId")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var nvQl = _context.NhanViens.Include(nv => nv.LoaiNvNavigation)
                                         .Where(nv => nv.LoaiNvNavigation.TenLoai.Contains("Quản lý"));
            ViewData["CaId"] = new SelectList(_context.CaLams, "CaId", "TenCa", nhanVien.CaId);
            ViewData["LoaiNv"] = new SelectList(_context.LoaiNhanViens, "LoaiNv", "TenLoai", nhanVien.LoaiNv);
            ViewData["NhanVienQl"] = new SelectList(nvQl, "NhanVienId", "TenNhanVien", nhanVien.NhanVienQl);
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            var nvQl = _context.NhanViens.Include(nv => nv.LoaiNvNavigation)
                                         .Where(nv => nv.LoaiNvNavigation.TenLoai.Contains("Quản lý"));
            ViewData["CaId"] = new SelectList(_context.CaLams, "CaId", "TenCa", nhanVien.CaId);
            ViewData["LoaiNv"] = new SelectList(_context.LoaiNhanViens, "LoaiNv", "TenLoai", nhanVien.LoaiNv);
            ViewData["NhanVienQl"] = new SelectList(nvQl, "NhanVienId", "TenNhanVien", nhanVien.NhanVienQl);
            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NhanVienId,TenNhanVien,Sdt,DiaChi,LoaiNv,NhanVienQl,CaId")] NhanVien nhanVien)
        {
            if (id != nhanVien.NhanVienId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.NhanVienId))
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
            var nvQl = _context.NhanViens.Include(nv => nv.LoaiNvNavigation)
                                         .Where(nv => nv.LoaiNvNavigation.TenLoai.Contains("Quản lý"));
            ViewData["CaId"] = new SelectList(_context.CaLams, "CaId", "TenCa", nhanVien.CaId);
            ViewData["LoaiNv"] = new SelectList(_context.LoaiNhanViens, "LoaiNv", "TenLoai", nhanVien.LoaiNv);
            ViewData["NhanVienQl"] = new SelectList(nvQl, "NhanVienId", "TenNhanVien", nhanVien.NhanVienQl);
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.Ca)
                .Include(n => n.LoaiNvNavigation)
                .Include(n => n.NhanVienQlNavigation)
                .FirstOrDefaultAsync(m => m.NhanVienId == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NhanViens == null)
            {
                return Problem("Entity set 'TamtentoiContext.NhanViens'  is null.");
            }
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien != null)
            {
                _context.NhanViens.Remove(nhanVien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(int id)
        {
          return (_context.NhanViens?.Any(e => e.NhanVienId == id)).GetValueOrDefault();
        }
    }
}
