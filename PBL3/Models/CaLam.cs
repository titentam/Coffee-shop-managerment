using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBL3.Models
{
    public partial class CaLam
    {
        public CaLam()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        [Display(Name = "Ca ID")]
        public int CaId { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage ="Vui lòng nhập tên ca")]
        [Display(Name = "Tên ca")]
        public string? TenCa { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập thời gian bắt đầu")]
        [Display(Name = "Thời gian bắt đầu")]
        public DateTime? ThoiGianBatDau { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập thời gian kết thúc")]
        [Display(Name = "Thời gian kết thúc")]
        public DateTime? ThoiGianKetThuc { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
