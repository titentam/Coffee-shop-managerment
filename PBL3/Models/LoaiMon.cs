using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class LoaiMon
    {
        public LoaiMon()
        {
            Mons = new HashSet<Mon>();
        }

        public int LoaiMonId { get; set; }
        public string? TenLoaiMon { get; set; }

        public virtual ICollection<Mon> Mons { get; set; }
    }
}
