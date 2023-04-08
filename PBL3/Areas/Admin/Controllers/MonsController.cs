using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using DiChoSaiGon.Helpper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using PBL3.Models;

namespace PBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MonsController : Controller
    {
        private readonly TamtentoiContext _context;
        public INotyfService _notifyService { get; }

        public MonsController(TamtentoiContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/Mons
        public IActionResult Index(int? page, string? search, int option = 0)
        {
            var pageNumber = page == null || page < 0 ? 1 : page.Value;
            var pageSize = 10;
            ViewData["CurrentSearch"] = search;
            ViewData["DsLoaiMon"] = new SelectList(_context.LoaiMons, "LoaiMonId", "TenLoaiMon", option);
            var list = _context.Mons.Include(n => n.LoaiMon)
                                           .Include(n => n.CongThuc)
                                           .OrderByDescending(n => n.MonId).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                list = list.Where(nv => nv.TenMon.Contains(search)).ToList();
            }
            if (option != 0)
            {
                list = list.Where(nv => nv.LoaiMonId == option).ToList();
            }
            PagedList<Mon> models = new PagedList<Mon>(list.AsQueryable(), pageNumber, pageSize);
            return View(models);
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
            ViewData["CongThucId"] = new SelectList(_context.CongThucs, "CongThucId", "TenCongThuc");
            ViewData["LoaiMonId"] = new SelectList(_context.LoaiMons, "LoaiMonId", "TenLoaiMon");
            return View();
        }

        // POST: Admin/Mons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MonId,TenMon,Gia,LoaiMonId,CongThucId,HinhAnh")] Mon mon, IFormFile? fThumb)
        {
            if (ModelState.IsValid)
            {
                mon.TenMon = Utilities.ToTitleCase(mon.TenMon);
                if (fThumb != null)
                {
                    string extention = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(mon.TenMon) + extention;
                    mon.HinhAnh = await Utilities.UploadFile(fThumb, @"mons", image.ToLower());

                }
                if (string.IsNullOrEmpty(mon.HinhAnh)) mon.HinhAnh = "default.jpg";
                _context.Add(mon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CongThucId"] = new SelectList(_context.CongThucs, "CongThucId", "TenCongThuc", mon.CongThucId);
            ViewData["LoaiMonId"] = new SelectList(_context.LoaiMons, "LoaiMonId", "TenLoaiMon", mon.LoaiMonId);
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
            ViewData["CongThucId"] = new SelectList(_context.CongThucs, "CongThucId", "TenCongThuc", mon.CongThucId);
            ViewData["LoaiMonId"] = new SelectList(_context.LoaiMons, "LoaiMonId", "TenLoaiMon", mon.LoaiMonId);
            return View(mon);
        }

        // POST: Admin/Mons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MonId,TenMon,Gia,LoaiMonId,CongThucId,HinhAnh")] Mon mon , IFormFile? fThumb)
        {
            if (id != mon.MonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					mon.TenMon = Utilities.ToTitleCase(mon.TenMon);
					if (fThumb != null)
					{
						string extention = Path.GetExtension(fThumb.FileName);
						string image = Utilities.SEOUrl(mon.TenMon) + extention;
						mon.HinhAnh = await Utilities.UploadFile(fThumb, @"mons", image.ToLower());

					}
					if (string.IsNullOrEmpty(mon.HinhAnh)) mon.HinhAnh = "default.jpg";
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
            ViewData["CongThucId"] = new SelectList(_context.CongThucs, "CongThucId", "TenCongThuc", mon.CongThucId);
            ViewData["LoaiMonId"] = new SelectList(_context.LoaiMons, "LoaiMonId", "TenLoaiMon", mon.LoaiMonId);
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
