using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class Ban
    {
        public Ban()
        {
            DonDatMons = new HashSet<DonDatMon>();
        }

        public int BanId { get; set; }
        public bool? TinhTrang { get; set; }

        public virtual ICollection<DonDatMon> DonDatMons { get; set; }
    }
}
