using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            DonDatMons = new HashSet<DonDatMon>();
            HoaDons = new HashSet<HoaDon>();
            NhapHangs = new HashSet<NhapHang>();
        }

        public string MaNv { get; set; } = null!;
        public string? HoTen { get; set; }
        public string? LoaiNv { get; set; }
        public string? DiaChi { get; set; }
        public string? Sdt { get; set; }
        public int? MaCa { get; set; }

        public virtual LoaiNv? LoaiNvNavigation { get; set; }
        public virtual CaLamViec? MaCaNavigation { get; set; }
        public virtual ICollection<DonDatMon> DonDatMons { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<NhapHang> NhapHangs { get; set; }
    }
}
