using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PBL3.Models
{
    [Keyless]
	public class Date
	{
		[Required(AllowEmptyStrings = true, ErrorMessage =" ")]
		[Range(1, 12, ErrorMessage = "Vui lòng nhập tháng hợp lệ")]
		[Display(Name = "Tháng")]
		public int? Month { get; set; }
		[Display(Name = "Năm")]
		public int? Year { get; set; }
    }
}
