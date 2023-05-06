using Microsoft.EntityFrameworkCore;

namespace PBL3.Models
{
    [Keyless]
    public class SpLoaiMon
    {
        public int LoaiMonId { get; set; }
        public int SoLuong { get; set; }
    }
}
