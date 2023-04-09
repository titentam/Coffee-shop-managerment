using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class DonDatMon
    {
        public string MaDonDatMon { get; set; } = null!;
        public string? GhiChu { get; set; }
        public string? MaHd { get; set; }
        public string? MaBan { get; set; }
        public string? MaNv { get; set; }

        public virtual Ban? MaBanNavigation { get; set; }
        public virtual HoaDon? MaHdNavigation { get; set; }
        public virtual NhanVien? MaNvNavigation { get; set; }
    }
}
