using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PBL3.Models;
using PagedList.Core;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace PBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanViensController : Controller
    {
        private readonly TamtentoiContext _context;
		public INotyfService _notifyService { get; }

		public NhanViensController(TamtentoiContext context, INotyfService notifyService)
        {
            _context = context;
			_notifyService = notifyService;
		}

        // GET: Admin/NhanViens
        public async Task<IActionResult> Index(int? page, string ?search, int option =0)
        {
			var pageNumber = page == null || page < 0 ? 1 : page.Value;
			var pageSize = 7;
			ViewData["CurrentSearch"] = search;
			ViewData["DsLoaiNV"] = new SelectList(_context.LoaiNhanViens, "LoaiNv", "TenLoai", option);
            var listNv = _context.NhanViens.Include(n => n.Ca)
                                           .Include(n => n.LoaiNvNavigation)
                                           .Include(n => n.NhanVienQlNavigation)
                                           .OrderByDescending(n=>n.NhanVienId).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                listNv = listNv.Where(nv => nv.TenNhanVien.Contains(search)).ToList();
			}
            if (option != 0)
            {
                listNv = listNv.Where(nv => nv.LoaiNv == option).ToList();
            }
            PagedList<NhanVien> models = new PagedList<NhanVien>(listNv.AsQueryable(), pageNumber, pageSize);  
            return View(models);
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
                                         .Where(nv => nv.LoaiNvNavigation.TenLoai.Contains("Quản lý")).ToList();
            ViewData["NhanVienQl"] = new SelectList(nvQl, "NhanVienId", "TenNhanVien");
            ViewData["TaiKhoan"] = new TaiKhoan();
            return View();
        }

        // POST: Admin/NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NhanVienId,TenNhanVien,Sdt,DiaChi,LoaiNv,NhanVienQl,CaId")] NhanVien nhanVien, TaiKhoan acc)
        {
            ViewBag.TrungTaiKhoan = "";
            // check tk
            var valid = _context.TaiKhoans.Find(acc.TaiKhoan1);
            if (valid == null)
            {
                if (ModelState.IsValid)
                {
                    if (nhanVien.NhanVienQl == 0) nhanVien.NhanVienQl = null;
                    _context.Add(nhanVien);
                    _context.SaveChanges();
                    acc.NhanVienId = _context.NhanViens.ToList().OrderByDescending(nv => nv.NhanVienId).FirstOrDefault().NhanVienId;
                    _context.Add(acc);
                    _context.SaveChanges();
					_notifyService.Success("Thêm thành công!");
					return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ViewBag.TrungTaiKhoan = "Tài khoản này đã tồn tại";
            }
            var nvQl = _context.NhanViens.Include(nv => nv.LoaiNvNavigation)
                                         .Where(nv => nv.LoaiNvNavigation.TenLoai.Contains("Quản lý"));
            ViewData["CaId"] = new SelectList(_context.CaLams, "CaId", "TenCa", nhanVien.CaId);
            ViewData["LoaiNv"] = new SelectList(_context.LoaiNhanViens, "LoaiNv", "TenLoai", nhanVien.LoaiNv);
            ViewData["NhanVienQl"] = new SelectList(nvQl, "NhanVienId", "TenNhanVien", nhanVien.NhanVienQl);
            ViewData["TaiKhoan"] = acc;
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
                    if (nhanVien.NhanVienQl == 0) nhanVien.NhanVienQl = null;
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
					_notifyService.Success("Cập nhật thành công!");
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
                var listNv = _context.NhanViens.Where(nv=>nv.NhanVienQl==id).ToList();
                if (listNv != null)
                {
                    foreach(var item in listNv)
                    {
                        item.NhanVienQl = null;
                    }
                    await _context.SaveChangesAsync();
                } 
                _context.NhanViens.Remove(nhanVien);

            }

            await _context.SaveChangesAsync();
			_notifyService.Success("Xoá thành công!");
			return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(int id)
        {
            return (_context.NhanViens?.Any(e => e.NhanVienId == id)).GetValueOrDefault();
        }
    }
}
