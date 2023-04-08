using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class LoaiNv
    {
        public LoaiNv()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public string MaLoai { get; set; } = null!;
        public string LoaiNv1 { get; set; } = null!;

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
