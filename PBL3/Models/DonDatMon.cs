using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class DonDatMon
    {
        public DonDatMon()
        {
            MonDonDatMons = new HashSet<MonDonDatMon>();
        }

        public int DonDatMonId { get; set; }
        public string? GhiChu { get; set; }
        public int? HoaDonId { get; set; }
        public int? BanId { get; set; }
        public int? NhanVienId { get; set; }

        public virtual Ban? Ban { get; set; }
        public virtual HoaDon? HoaDon { get; set; }
        public virtual NhanVien? NhanVien { get; set; }
        public virtual ICollection<MonDonDatMon> MonDonDatMons { get; set; }
    }
}
