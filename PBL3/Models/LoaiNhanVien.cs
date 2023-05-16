using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PBL3.Models
{
    public partial class LoaiNhanVien
    {
        public LoaiNhanVien()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public int LoaiNv { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên loại nhân viên")]
        [Display(Name = "Tên loại nhân viên")]
        public string? TenLoai { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
