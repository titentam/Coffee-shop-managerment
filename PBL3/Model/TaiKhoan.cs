using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class TaiKhoan
    {
        public string? MaNv { get; set; }
        public string? TaiKhoan1 { get; set; }
        public string? MatKhau { get; set; }

        public virtual NhanVien? MaNvNavigation { get; set; }
    }
}
