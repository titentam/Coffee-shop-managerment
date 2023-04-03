using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class CaLam
    {
        public CaLam()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public int CaId { get; set; }
        public string? TenCa { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
