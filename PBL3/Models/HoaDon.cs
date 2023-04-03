using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            DonDatMons = new HashSet<DonDatMon>();
        }

        public int HoaDonId { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public decimal? TongTien { get; set; }
        public bool? TrangThaiThanhToan { get; set; }
        public int? NhanVienId { get; set; }

        public virtual NhanVien? NhanVien { get; set; }
        public virtual ICollection<DonDatMon> DonDatMons { get; set; }
    }
}
