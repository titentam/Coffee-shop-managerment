using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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
        [Required(ErrorMessage = "Vui lòng nhập tên nguyên liệu")]
        [Display(Name = "Tên nguyên liệu")]
        public string? TenNguyenLieu { get; set; }
        [Display(Name = "Tên món")]
        public int? SoLuong { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Display(Name = "Giá")]
        public decimal? Gia { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập đơn vị tính")]
        [Display(Name = "Đơn vị tính")]
        public string? DonViTinh { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp")]
        [Display(Name = "Nhà cung cấp")]
        public int? NhaCungCapId { get; set; }

        public virtual NhaCungCap? NhaCungCap { get; set; }
        public virtual ICollection<CongThucNguyenLieu> CongThucNguyenLieus { get; set; }
        public virtual ICollection<DatHangNguyenLieu> DatHangNguyenLieus { get; set; }
    }
}
