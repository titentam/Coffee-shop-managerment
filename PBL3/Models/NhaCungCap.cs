using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PBL3.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            NguyenLieus = new HashSet<NguyenLieu>();
        }

        public int NhaCungCapId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên nhà cung cấp")]
        [Display(Name = "Tên nhà cung cấp")]
        public string? TenNhaCungCap { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Display(Name = "Số điện thoại")]
        [Phone]
        public string? Sdt { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        public virtual ICollection<NguyenLieu> NguyenLieus { get; set; }
    }
}
