using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class CaLamViec
    {
        public CaLamViec()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public int MaCa { get; set; }
        public string? TenCa { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
