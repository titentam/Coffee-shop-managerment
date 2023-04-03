using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class NguyenLieu
    {
        public NguyenLieu()
        {
            CongThucNguyenLieus = new HashSet<CongThucNguyenLieu>();
            DatHangNguyenLieus = new HashSet<DatHangNguyenLieu>();
        }

        public int NguyenLieuId { get; set; }
        public string? TenNguyenLieu { get; set; }
        public int? SoLuong { get; set; }
        public decimal? Gia { get; set; }
        public string? DonViTinh { get; set; }
        public int? NhaCungCapId { get; set; }

        public virtual NhaCungCap? NhaCungCap { get; set; }
        public virtual ICollection<CongThucNguyenLieu> CongThucNguyenLieus { get; set; }
        public virtual ICollection<DatHangNguyenLieu> DatHangNguyenLieus { get; set; }
    }
}
