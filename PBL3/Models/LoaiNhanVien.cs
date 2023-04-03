using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class LoaiNhanVien
    {
        public LoaiNhanVien()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public int LoaiNv { get; set; }
        public string? TenLoai { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
