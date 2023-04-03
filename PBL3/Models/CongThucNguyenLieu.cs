using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class CongThucNguyenLieu
    {
        public int? SoLuong { get; set; }
        public int NguyenLieuId { get; set; }
        public int CongThucId { get; set; }

        public virtual CongThuc CongThuc { get; set; } = null!;
        public virtual NguyenLieu NguyenLieu { get; set; } = null!;
    }
}
