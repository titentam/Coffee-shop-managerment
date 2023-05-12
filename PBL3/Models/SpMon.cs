using Microsoft.EntityFrameworkCore;

namespace PBL3.Models
{
    [Keyless]
    public class SpMon
    {
        public int MonId { get; set; }
        public int SoLuong { get; set; }
    }
}
