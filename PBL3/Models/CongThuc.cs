using System;
using System.Collections.Generic;

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
        public string? TenCongThuc { get; set; }

        public virtual ICollection<CongThucNguyenLieu> CongThucNguyenLieus { get; set; }
        public virtual ICollection<Mon> Mons { get; set; }
    }
}
