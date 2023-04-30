using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class MonDonDatMon
    {
        public int? SoLuong { get; set; }
        public int MonId { get; set; }
        public int DonDatMonId { get; set; }
        public bool? TinhTrang { get; set; }

        public virtual DonDatMon DonDatMon { get; set; } = null!;
        public virtual Mon Mon { get; set; } = null!;
    }
}
