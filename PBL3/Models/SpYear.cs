using Microsoft.EntityFrameworkCore;

namespace PBL3.Models
{
	[Keyless]
	public class SpYear
	{
        public int Year { get; set; }
        public decimal Total { get; set; }
    }
}
