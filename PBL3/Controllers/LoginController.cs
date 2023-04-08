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
            Account acc = new Account();
            return View();
        }
        [HttpPost]
        public IActionResult Index(Account acc)
        {
            PBL3Context PBL3Context = new PBL3Context();
            var status  = PBL3Context.TaiKhoans.Where(m=> m.TaiKhoan1 == acc.UserName && m.MatKhau == acc.Password).FirstOrDefault();
            if (status == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View(acc);
        }


        
        
        
    }
}
