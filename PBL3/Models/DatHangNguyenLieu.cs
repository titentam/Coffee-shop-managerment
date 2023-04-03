using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class DatHangNguyenLieu
    {
        public int? SoLuong { get; set; }
        public decimal? TongGia { get; set; }
        public int NguyenLieuId { get; set; }
        public int DathangId { get; set; }

        public virtual DatHang Dathang { get; set; } = null!;
        public virtual NguyenLieu NguyenLieu { get; set; } = null!;
    }
}
