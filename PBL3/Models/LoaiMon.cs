using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PBL3.Models
{
    public partial class LoaiMon
    {
        public LoaiMon()
        {
            Mons = new HashSet<Mon>();
        }

        public int LoaiMonId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên loại món")]
        [Display(Name = "Tên loại món")]
        public string? TenLoaiMon { get; set; }

        public virtual ICollection<Mon> Mons { get; set; }
    }
}
