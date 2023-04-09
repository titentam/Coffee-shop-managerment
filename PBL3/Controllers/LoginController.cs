using Microsoft.AspNetCore.Mvc;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PBL3.AccountModel;

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

        public IActionResult Index(TaiKhoan? _acc)
        {
            if( _acc == null ) _acc= new TaiKhoan();
            return View(_acc);
        }
        [HttpPost]
        public IActionResult Login(TaiKhoan _acc)
        {
            //var acc  = _context.TaiKhoans.Where(m=> m.TaiKhoan1 == _acc.TaiKhoan1 && m.MatKhau == _acc.MatKhau).FirstOrDefault();
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

            var role = _context.LoaiNhanViens.FromSqlRaw($"GetLoaiNV '{_acc.TaiKhoan1}', '{_acc.MatKhau}'").ToList().SingleOrDefault();
            if (role == null)
            {
                ViewBag.LoginStatus = 0;
                    
            }
            else
            {
                if(role.TenLoai=="Quản lý")
                {
                    return RedirectToAction("index", "Home", new { area = "Admin" });
                }
            }
            return RedirectToAction("index");
        }
    }
}
