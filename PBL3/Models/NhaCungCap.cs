using System;
using System.Collections.Generic;

namespace PBL3.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            NguyenLieus = new HashSet<NguyenLieu>();
        }

        public int NhaCungCapId { get; set; }
        public string? TenNhaCungCap { get; set; }
        public string? Sdt { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<NguyenLieu> NguyenLieus { get; set; }
    }
}
