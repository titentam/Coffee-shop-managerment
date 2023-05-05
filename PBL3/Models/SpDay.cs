using Microsoft.EntityFrameworkCore;

namespace PBL3.Models
{
    [Keyless]
    public class SpDay
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Total { get; set; }
    }
}