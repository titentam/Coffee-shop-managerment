using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class Mon
    {
        public string MaMon { get; set; } = null!;
        public string MaCongThuc { get; set; } = null!;
        public string? Ten { get; set; }
        public string? Loai { get; set; }
        public decimal? Gia { get; set; }

        public virtual LoaiMon? LoaiNavigation { get; set; }
        public virtual CongThuc MaCongThucNavigation { get; set; } = null!;
    }
}
