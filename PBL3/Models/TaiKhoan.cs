using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class TaiKhoan
    {
        public string TaiKhoan1 { get; set; } = null!;
        public string? MatKhau { get; set; }
        public int? NhanVienId { get; set; }

        public virtual NhanVien? NhanVien { get; set; }
    }
}
