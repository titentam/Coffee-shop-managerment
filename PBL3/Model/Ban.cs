using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class Ban
    {
        public Ban()
        {
            DonDatMons = new HashSet<DonDatMon>();
        }

        public string MaBan { get; set; } = null!;
        public int? Ban1 { get; set; }
        public int? Tang { get; set; }
        public bool? TinhTrang { get; set; }

        public virtual ICollection<DonDatMon> DonDatMons { get; set; }
    }
}
