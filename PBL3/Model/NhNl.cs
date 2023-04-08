using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class NhNl
    {
        public string MaDonHang { get; set; } = null!;
        public string? MaNguyenLieu { get; set; }
        public double? SoLuong { get; set; }
        public decimal? TongGia { get; set; }

        public virtual NhapHang MaDonHangNavigation { get; set; } = null!;
        public virtual NguyenLieu? MaNguyenLieuNavigation { get; set; }
    }
}
