using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            DonDatMons = new HashSet<DonDatMon>();
        }

        public string MaHd { get; set; } = null!;
        public string MaNv { get; set; } = null!;
        public DateTime? ThoiGianTao { get; set; }
        public decimal? TongTien { get; set; }
        public string? Vat { get; set; }
        public bool? TrangThaiThanhToan { get; set; }

        public virtual NhanVien MaNvNavigation { get; set; } = null!;
        public virtual ICollection<DonDatMon> DonDatMons { get; set; }
    }
}
