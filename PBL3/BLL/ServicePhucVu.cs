using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PBL3.Models;
using static PBL3.Areas.Serve.Controllers.HomeController;

namespace PBL3.BLL
{
    public static class ServicePhucVu
    {
        public static void ChuyenPhaChe(int banId, int nvId, List<MonDonDatMon> listItem)
        {
            using(TamtentoiContext db = new TamtentoiContext())
            {
                var order = new DonDatMon();
                order.HoaDonId = null;
                order.BanId = banId<=0? null:banId;
                order.NhanVienId = nvId;
                order.TinhTrang = false;

                db.Add(order);
                db.SaveChanges();

				int orderId = db.DonDatMons.AsNoTracking().ToList().MaxBy(x => x.DonDatMonId).DonDatMonId;
				foreach (var item in listItem)
                {
                    
                    item.DonDatMonId = orderId;
                }

                db.AddRange(listItem);
                db.SaveChanges();

            }
        }

        public static int AddToCart(int idItem, int banId, int nvId, int orderIdCurrent = 0 )
        {
            using (TamtentoiContext db = new TamtentoiContext())
            {
                if(orderIdCurrent==0)
                {
                    var order = new DonDatMon();
                    order.HoaDonId = null;
                    order.BanId = banId <= 0 ? null : banId;
                    order.NhanVienId = nvId;
                    order.TinhTrang = false;

                    db.Add(order);
                    db.SaveChanges();


                    int orderId = db.DonDatMons.AsNoTracking().ToList().MaxBy(x => x.DonDatMonId).DonDatMonId;
                    var item = new MonDonDatMon()
                    {
                        MonId = idItem,
                        SoLuong = 1,
                        DonDatMonId = orderId
                    };
                    db.Add(item);
                    db.SaveChanges();
                    return orderId;
                }

                var item2 = new MonDonDatMon()
                {
                    MonId = idItem,
                    SoLuong = 1,
                    DonDatMonId = orderIdCurrent
                };
                db.Add(item2);
                db.SaveChanges();
                return orderIdCurrent;
            }
        }
        public static void UpdateToCart(int idItem, int orderId)
        {
            using (TamtentoiContext db = new TamtentoiContext())
            {
               var item = db.MonDonDatMons.Where(x=>x.DonDatMonId == orderId && x.MonId == idItem).FirstOrDefault();
                item.SoLuong += 1;
                db.SaveChanges();
            }
        }
        public static List<Tuple<Mon, int>> GetOrderDetails(int orderId)
        {
            using (TamtentoiContext db = new TamtentoiContext())
            {
                List<Tuple<Mon,int>> res = new List<Tuple<Mon,int>>();  
                var items = db.MonDonDatMons.AsNoTracking()
                    .Where(item=>item.DonDatMonId ==orderId).ToList();
                foreach(var item in items)
                {
                    var mon = db.Mons.AsNoTracking()
                        .Where(x=>x.MonId ==item.MonId).SingleOrDefault();
                    int quantity = item.SoLuong ?? 0;
                    if (mon!= null)
                    {
                        res.Add(new Tuple<Mon,int>(mon,quantity));
                    }
                }
                return res;
            }
        }
        public static void PayTheBill(int orderId, int nvId)
        {
            using (TamtentoiContext db = new TamtentoiContext())
            {
                var items = db.MonDonDatMons.AsNoTracking()
                    .Where(item => item.DonDatMonId == orderId).ToList();
                decimal total = 0;
                foreach (var item in items)
                { 
                    var mon = db.Mons.AsNoTracking()
                        .Where(x => x.MonId == item.MonId).SingleOrDefault();
                    int quantity = item.SoLuong ?? 0;
                    if (mon != null)
                    {
                        total += (mon.Gia??0) * quantity; 
                    }
                }

                var invoice = new HoaDon()
                {
                    ThoiGianTao = DateTime.Now,
                    TongTien = total,
                    TrangThaiThanhToan = true,
                    NhanVienId = nvId,
                };

                db.Add(invoice);
                db.SaveChanges();

                var invoiceId = db.HoaDons.AsNoTracking().ToList().MaxBy(x => x.HoaDonId).HoaDonId;
                var order = db.DonDatMons.Where(x => x.DonDatMonId == orderId).SingleOrDefault();
                order.HoaDonId = invoiceId;
                db.SaveChanges();
            }
        }
    }
}
