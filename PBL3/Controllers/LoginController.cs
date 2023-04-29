using DiChoSaiGon.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PBL3.Models;
using System.Security.Principal;
namespace PBL3.Controllers
{
    public class LoginController : Controller
    {
        private readonly TamtentoiContext _context;

        public LoginController(TamtentoiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(TaiKhoan acc)
        {
            //var acc  = _context.TaiKhoans.Where(m=> m.TaiKhoan1 == acc.TaiKhoan1 && m.MatKhau == acc.MatKhau).FirstOrDefault();
            //if (acc == null)
            //{
            //    ViewBag.LoginStatus = 0;
            //}
            //else
            //{
            //    var nv = _context.NhanViens.Where(n => n.NhanVienId == acc.NhanVienId).SingleOrDefault(); 
            //    var role = _context.LoaiNhanViens.Where(l=>l.LoaiNv==nv.LoaiNv).SingleOrDefault();
            //    if (role.TenLoai == "Quản lý")
            //    {
            //        return RedirectToAction("index", "Home", new { area = "Admin" });
            //    }   
            //}
            //return RedirectToAction("index");

            var role = _context.LoaiNhanViens.FromSqlRaw($"GetLoaiNV '{acc.TaiKhoan1}', '{acc.MatKhau}'").ToList().SingleOrDefault();
            if (role == null)
            {
                ViewBag.error = "Tài khoản và mật khẩu không đúng"; 
            }
            else
            {
                if(role.TenLoai=="Quản lý")
                {
                    var id = _context.TaiKhoans.Find(acc.TaiKhoan1).NhanVienId;
                    HttpContext.Session.SetInt32("user", (int)id);
                    return RedirectToAction("index", "Home", new { area = "Admin" });
                }
                if(role.TenLoai=="Phục vụ")
                {
                    var id = _context.TaiKhoans.Find(acc.TaiKhoan1).NhanVienId;
                    HttpContext.Session.SetInt32("user", (int)id);
                    return RedirectToAction("index", "Home", new { area = "Serve" });
                }
				if (role.TenLoai=="Pha chế")
				{
					var id = _context.TaiKhoans.Find(acc.TaiKhoan1).NhanVienId;
					HttpContext.Session.SetInt32("user", (int)id);
					return RedirectToAction("index", "Home", new { area = "Bartender" });
				}

			}
            return View(acc);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index");
            
        }
    }
}
