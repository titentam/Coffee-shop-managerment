using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class NguyenLieu
    {
        public string MaNguyenLieu { get; set; } = null!;
        public string? TenNguyenLieu { get; set; }
        public double? SoLuong { get; set; }
        public string? Dvt { get; set; }
        public decimal? DonGia { get; set; }
        public string? MaNhaCc { get; set; }

        public virtual NhaCungCap? MaNhaCcNavigation { get; set; }
    }
}
