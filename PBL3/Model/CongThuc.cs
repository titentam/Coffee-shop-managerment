using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class CongThuc
    {
        public CongThuc()
        {
            Mons = new HashSet<Mon>();
        }

        public string MaCongThuc { get; set; } = null!;
        public string? TenCongThuc { get; set; }
        public string? CongThuc1 { get; set; }

        public virtual ICollection<Mon> Mons { get; set; }
    }
}
