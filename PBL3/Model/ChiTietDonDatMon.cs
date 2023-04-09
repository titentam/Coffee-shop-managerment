using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class ChiTietDonDatMon
    {
        public string MaMon { get; set; } = null!;
        public string? MaDonDatMon { get; set; }
        public double? SoLuong { get; set; }

        public virtual DonDatMon? MaDonDatMonNavigation { get; set; }
        public virtual Mon MaMonNavigation { get; set; } = null!;
    }
}
