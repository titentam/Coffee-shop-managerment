using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class NhapHang
    {
        public string MaDonHang { get; set; } = null!;
        public DateTime? NgayDatHang { get; set; }
        public string? GhiChu { get; set; }
        public string? MaNv { get; set; }

        public virtual NhanVien? MaNvNavigation { get; set; }
    }
}
