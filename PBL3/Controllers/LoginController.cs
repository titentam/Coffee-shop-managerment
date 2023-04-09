using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PBL3.AccountModel;
using PBL3.Model;
using System.Security.Principal;
namespace PBL3.Controllers
{
    public class LoginController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            Account _acc = new Account();
            return View();
        }
        [HttpPost]
        public IActionResult Login(Account _acc)
        {
            PBL3Context _context = new PBL3Context();
            _context = new PBL3Context();
            var status  = _context.TaiKhoans.Where(m=> m.TaiKhoan1 == _acc.UserName && m.MatKhau == _acc.Password).FirstOrDefault();
            if (status == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_acc);
        }
    }
}
