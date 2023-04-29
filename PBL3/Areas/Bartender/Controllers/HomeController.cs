using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.Models;
using System.Web;

namespace PBL3.Areas.Bartender.Controllers
{
    [Area("Bartender")]
    public class HomeController : Controller
    {
        private readonly TamtentoiContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public INotyfService _notifyService { get; }
        public HomeController(TamtentoiContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        public IActionResult Index()
        {
           var list= _context.DonDatMons.ToList();
            
            return View( list);
        }

    }
}
