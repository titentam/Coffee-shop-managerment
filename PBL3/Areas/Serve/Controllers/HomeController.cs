using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using PBL3.BLL;
using PBL3.Models;
using System.Security.Policy;
using static PBL3.Areas.Serve.Controllers.HomeController;

namespace PBL3.Areas.Serve.Controllers
{
    [Area("Serve")]
    public class HomeController : Controller
    {
        private readonly TamtentoiContext _context;
        public INotyfService _notifyService { get; }
        public static List<Tuple<int,int>> _BanDetails; // store id dondatmon

        public List<Tuple<int, int>> BanDetails
        {
            get
            {
                if (_BanDetails == null)
                {
                    _BanDetails = new List<Tuple<int, int>>();
                }
                return _BanDetails;
            }
            set
            {
                _BanDetails = value;
            }
        }
        public HomeController(TamtentoiContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService; 
        }
        public class OrderID
        {
            public int Id { get; set; }
            public int BanId { get; set; }
            public int NvID { get; set; }   
        }
        public class BanSelected
        {
            public int BanId { get; set; }
        }
        public IActionResult Index(int cbbSortMon = -1, int cbbChon_Ban = -1, string search = "" )
        {
            ViewBag.listLoaiMon = new SelectList(_context.LoaiMons, "LoaiMonId", "TenLoaiMon", cbbSortMon);
            ViewBag.listBan = new SelectList(_context.Bans, "BanId", "BanId", cbbChon_Ban);
            var list = _context.Mons.OrderBy(n => n.MonId).ToList();
            if(cbbSortMon != -1)
            {
                list = list.Where(x=> x.LoaiMonId == cbbSortMon).ToList();
            }
            if(!string.IsNullOrEmpty(search))
            {
                list = list.Where(x => x.TenMon.Contains(search)).ToList();
                ViewBag.search = search;    
            }
            return View(list);
        }

        [HttpPost]
        [Route("api/UpdateToCart")]
        public IActionResult UpdateToCart([FromBody] OrderID request)
        {
            int id = request.Id;
            int banId = request.BanId;
            var banDetails = BanDetails.Where(x => x.Item1 == banId).SingleOrDefault();

            // double click lần đầu nhanh qua là bị lỗi => bấm từ từ thôi :)))
            if(banDetails != null) 
            {
                var orderId = banDetails.Item2;
                ServicePhucVu.UpdateToCart(id, orderId);
            }
        
            return Ok(new { id = id });
        }
        

        [HttpPost]
        [Route("api/AddToCart")]
        public IActionResult AddToCart([FromBody] OrderID request)
        {
            int id = request.Id;
            int banId = request.BanId;
            int nvId = request.NvID;
            var banDetails = BanDetails.Where(x=>x.Item1 == banId).SingleOrDefault();
            if(banDetails == null)
            {
                int orderId = ServicePhucVu.AddToCart(id, banId, nvId);
                BanDetails.Add(new Tuple<int, int>(banId, orderId));
                
            }
            else
            {
                ServicePhucVu.AddToCart(id, banId, nvId, banDetails.Item2);
            }
            return Ok(new { id = id });
        }

        public IActionResult ChuyenPhaChe(int banId, int nvId)
        {

            
            return RedirectToAction("index");

        }

        [HttpPost]
        [Route("api/LoadOrderDetails")]
        public IActionResult LoadOrderDetails([FromBody] BanSelected model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data");
            }

            int banId = model.BanId;

            var banDetail = BanDetails.FirstOrDefault(x => x.Item1 == banId);
            if (banDetail == null)
            {
                return PartialView("_PartialViewOrderDetails", null);
            }

            int orderId = banDetail.Item2;

            var items = ServicePhucVu.GetOrderDetails(orderId);
            return PartialView("_PartialViewOrderDetails", items);
        }
        [HttpPost]
        public IActionResult PayTheBill(int banId, int nvId)
        {
            var banDetail = BanDetails?.SingleOrDefault(x => x.Item1 == banId);
            if (banDetail != null)
            {
                ServicePhucVu.PayTheBill(banDetail.Item2, nvId);
                BanDetails.Remove(banDetail);
                _notifyService.Success("Thanh toán thành công");
                return Ok();
            }
            return BadRequest();
        }
    }
}
