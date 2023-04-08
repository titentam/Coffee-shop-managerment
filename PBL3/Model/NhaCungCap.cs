using System;
using System.Collections.Generic;

namespace PBL3.Model
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            NguyenLieus = new HashSet<NguyenLieu>();
        }

        public string MaNhaCc { get; set; } = null!;
        public string? TenNhaCc { get; set; }
        public string Sdt { get; set; } = null!;
        public string? Email { get; set; }

        public virtual ICollection<NguyenLieu> NguyenLieus { get; set; }
    }
}
