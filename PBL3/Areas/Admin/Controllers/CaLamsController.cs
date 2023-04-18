using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PagedList.Core;
using PBL3.Models;

namespace PBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CaLamsController : Controller
    {
        private readonly TamtentoiContext _context;
        public INotyfService _notifyService { get; }
        public CaLamsController(TamtentoiContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/CaLams
		public IActionResult Index(string? search)
		{
            ViewData["CurrentSearch"] = search;
			var list = _context.CaLams.OrderByDescending(n => n.CaId).ToList();
			if (!string.IsNullOrEmpty(search))
			{
				list = list.Where(x => x.TenCa.Contains(search)).ToList();
			}
			return View(list);
		}

		// GET: Admin/CaLams/Details/5
		public async Task<IActionResult> Details(int? id, string? search)
        {
			ViewData["CurrentSearch"] = search;
			if (id == null || _context.CaLams == null)
            {
                return NotFound();
            }

            var caLam = await _context.CaLams
                .Include(x =>x.NhanViens)
                .FirstOrDefaultAsync(m => m.CaId == id);
			if (caLam == null)
			{
				return NotFound();
			}

			var listNv = caLam.NhanViens.ToList()
                .Join(_context.LoaiNhanViens, nv=>nv.LoaiNv, loainv => loainv.LoaiNv,(nv,loainv)=>nv)
                .ToList();
			if (!string.IsNullOrEmpty(search))
			{
				listNv = listNv.Where(x => x.TenNhanVien.Contains(search)).ToList();
			}
            listNv = listNv.OrderByDescending(nv => nv.NhanVienId).ToList();

			ViewBag.listNv = listNv;
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
				_notifyService.Success("Thêm thành công!");
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
					_notifyService.Success("Cập nhật thành công!");
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
			_notifyService.Success("Xoá thành công!");
			return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteNv(int id, int nvId)
        {
            var nv = _context.NhanViens.Find(nvId);
            if (nv != null)
            {
                nv.CaId = null;
                _context.SaveChanges();
				_notifyService.Success("Xoá nhân viên thành công!");
			}
            return RedirectToAction("details", new { id = id });
        }
        public IActionResult AddNv(int id)
        {
            ViewBag.id = id;
            var listNv = _context.NhanViens.Include(nv=>nv.LoaiNvNavigation).Where(nv=>nv.CaId==null).OrderBy(nv=>nv.NhanVienId).ToList();
            return View(listNv);
        }

        [HttpPost]
		public IActionResult AddNv(int id, int idNv)
		{
            var nV = _context.NhanViens.Where(nv=>nv.NhanVienId==idNv).SingleOrDefault();
            if(nV != null)
            {
                nV.CaId = id;
                _context.SaveChanges();
				_notifyService.Success("Thêm nhân viên thành công!");
			}
			return RedirectToAction("AddNv", new {id = id});
		}

		private bool CaLamExists(int id)
        {
          return (_context.CaLams?.Any(e => e.CaId == id)).GetValueOrDefault();
        }
    }
}
