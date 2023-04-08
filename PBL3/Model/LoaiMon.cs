using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class LoaiMon
    {
        public LoaiMon()
        {
            Mons = new HashSet<Mon>();
        }

        public string MaLoai { get; set; } = null!;
        public string TenLoai { get; set; } = null!;

        public virtual ICollection<Mon> Mons { get; set; }
    }
}
