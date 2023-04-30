﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.Models;

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
            var list = _context.DonDatMons.Where(x=>x.TinhTrang==false).ToList();

            return View(list);
        }
        [HttpPost]
        public IActionResult ShowDanhSach(int id)
        {
            
            var listOrderDetail = _context.MonDonDatMons.Where(x=>x.DonDatMonId==id).ToList();
            List<Tuple<Mon, int>> listItem = new List<Tuple<Mon, int>>();
            foreach(var item in listOrderDetail)
            {
                var mon = _context.Mons.Find(item.MonId);
                if(mon!=null)
                {
                    listItem.Add(new Tuple<Mon,int>(mon,(int)item.SoLuong));
                }
            }
            return PartialView("_PartialViewDanhSachMon", listItem);
        }
        public IActionResult ShowCongThuc(int id)
        {

            var mon = _context.Mons.Find(id);
            List<Tuple<NguyenLieu, int>> listCTDetails = new List<Tuple<NguyenLieu, int>>();
            if (mon!=null)
            {
                var ListCT = _context.CongThucNguyenLieus.Where(x => x.CongThucId==mon.CongThucId).ToList();
                
                foreach (var item in ListCT)
                {
                    var nguyenLieu=_context.NguyenLieus.Find(item.NguyenLieuId);
                    listCTDetails.Add(new Tuple<NguyenLieu, int>(nguyenLieu, (int)item.SoLuong));
                }
            }    
            return PartialView("_PartialViewShowCongThuc", listCTDetails);
        }

    }
}