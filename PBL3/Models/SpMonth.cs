using Microsoft.EntityFrameworkCore;

namespace PBL3.Models
{
    [Keyless]
    public class SpMonth
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Total { get; set; }
    }
}
