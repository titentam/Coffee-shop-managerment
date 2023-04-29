using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class DatHang
    {
        public DatHang()
        {
            DatHangNguyenLieus = new HashSet<DatHangNguyenLieu>();
        }

        public int DathangId { get; set; }
        public DateTime? NgayDat { get; set; }
        public string? GhiChu { get; set; }
        public int? NhanVienId { get; set; }
        public bool? TinhTrangXacNhan { get; set; }

        public virtual NhanVien? NhanVien { get; set; }
        public virtual ICollection<DatHangNguyenLieu> DatHangNguyenLieus { get; set; }
    }
}
