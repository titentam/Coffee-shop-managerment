using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            DatHangs = new HashSet<DatHang>();
            DonDatMons = new HashSet<DonDatMon>();
            HoaDons = new HashSet<HoaDon>();
            InverseNhanVienQlNavigation = new HashSet<NhanVien>();
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        public int NhanVienId { get; set; }
        public string? TenNhanVien { get; set; }
        public string? Sdt { get; set; }
        public string? DiaChi { get; set; }
        public int? LoaiNv { get; set; }
        public int? NhanVienQl { get; set; }
        public int? CaId { get; set; }

        public virtual CaLam? Ca { get; set; }
        public virtual LoaiNhanVien? LoaiNvNavigation { get; set; }
        public virtual NhanVien? NhanVienQlNavigation { get; set; }
        public virtual ICollection<DatHang> DatHangs { get; set; }
        public virtual ICollection<DonDatMon> DonDatMons { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<NhanVien> InverseNhanVienQlNavigation { get; set; }
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
