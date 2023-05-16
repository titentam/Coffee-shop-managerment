
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBL3.Models
{
    public partial class CongThuc
    {
        public CongThuc()
        {
            CongThucNguyenLieus = new HashSet<CongThucNguyenLieu>();
            Mons = new HashSet<Mon>();
        }

        
        public int CongThucId { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên công thức")]
        [Display(Name ="Tên công thức")]
        public string? TenCongThuc { get; set; }

        public virtual ICollection<CongThucNguyenLieu> CongThucNguyenLieus { get; set; }
        public virtual ICollection<Mon> Mons { get; set; }
    }
}
