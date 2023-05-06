﻿using Microsoft.AspNetCore.Mvc;
using PBL3.Models;

namespace PBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        private readonly TamtentoiContext _context;
        public ThongKeController(TamtentoiContext context)
        {
            _context = context;
        }
        public IActionResult Index(int sttByYear = 0)
        {
            if (HttpContext.Session.GetInt32("user") == null) return RedirectToAction("index", "Login", new { area = "" });
            if (sttByYear==0)
            {
                var d = DateTime.Today;
                var statistic = _context.StatisticByDay(d.Month,d.Year);
                ViewBag.Data = statistic;
                ViewBag.Option = 1;
                ViewBag.LabelString = $"Ngày trong tháng {d.Month}/{d.Year}";
            }
            else
            {
                var statistic = _context.StatisticByYear();
                ViewBag.Data = statistic;
                ViewBag.Option = 3;
                ViewBag.LabelString = $"Các năm gần đây";
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Day,Month,Year")]Date d)
        {
            if (HttpContext.Session.GetInt32("user") == null) return RedirectToAction("index", "Login", new { area = "" });

            // thống kê tháng trong năm 
            if (d.Month == null && d.Year != null)
            {
                var statistic = _context.StatisticByMonth().Where(x => x.Year == d.Year).ToList();
                ViewBag.Data = statistic;
                ViewBag.Option = 2;
                ViewBag.LabelString = $"Tháng trong năm {d.Year}";
            }
            // thống kê ngày trong tháng trong năm hiện tại
            else if (d.Month != null && d.Year == null)
            {
                var statistic = _context.StatisticByDay((int)d.Month, DateTime.Now.Year);
                ViewBag.Data = statistic;
                ViewBag.Option = 1;
                ViewBag.LabelString = $"Ngày trong tháng {d.Month}/{DateTime.Now.Year}";
            }
            // thống kê ngày trong tháng trong năm 
            else if (d.Month != null && d.Year != null)
            {
                var statistic = _context.StatisticByDay((int)d.Month, (int)d.Year);
                ViewBag.Data = statistic;
                ViewBag.Option = 1;
                ViewBag.LabelString = $"Ngày trong tháng {d.Month}/{d.Year}";
            } 
            else
            {
                return RedirectToAction("index");
            }

            return View(d);
        }
        
        public IActionResult ThongKeTheoLoaiMon(int option = 0)
        {
            int month = DateTime.Today.Month, year =DateTime.Today.Year;
            if (option == 1)// thang truoc
            {
                if(month == 1)
                {
                    month = 12;
                    year -= 1;
                }
                else
                    month -= 1;
            }
            var listLoaiMonId = _context.StatisticByLoaiMonId(month, year);
            if(listLoaiMonId != null)
            {
                List<Tuple<string, int>> model = new List<Tuple<string, int>>();
                foreach(var item in listLoaiMonId)
                {
                    var loaiMon = _context.LoaiMons.Find(item.LoaiMonId);
                    if (loaiMon != null)
                    {
                        model.Add(new Tuple<string, int>(loaiMon.TenLoaiMon, item.SoLuong));
                    }
                }
                return View(model);
            }
            return View(null);
        }
    }
}
