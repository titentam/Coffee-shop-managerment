using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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
        [Required(ErrorMessage = "Vui lòng nhập tên nhân viên")]
        [Display(Name = "Tên nhân viên")]
        public string? TenNhanVien { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại ")]
        [Display(Name = "Số điện thoại")]
        [Phone]
        public string? Sdt { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn loại nhân viên")]
        [Display(Name = "Loại nhân viên")]
        public int? LoaiNv { get; set; }
        
        [Display(Name = "Nhân viên quản lí")]
        public int? NhanVienQl { get; set; }
        
        [Display(Name = "Ca làm việc")]
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
