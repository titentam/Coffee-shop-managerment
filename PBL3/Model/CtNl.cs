using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class CtNl
    {
        public string? MaCongThuc { get; set; }
        public string? MaNguyenLieu { get; set; }
        public double? SoLuong { get; set; }

        public virtual CongThuc? MaCongThucNavigation { get; set; }
        public virtual NguyenLieu? MaNguyenLieuNavigation { get; set; }
    }
}
