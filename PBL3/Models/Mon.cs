using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBL3.Models
{
    public partial class Mon
    {
        public Mon()
        {
            MonDonDatMons = new HashSet<MonDonDatMon>();
        }

        public int MonId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên món")]
        [Display(Name = "Tên món")]
        public string? TenMon { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập giá")]
        [Display(Name = "Giá")]
		public decimal? Gia { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn loại món")]
        [Display(Name = "Loại món")]
        public int? LoaiMonId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn công thức")]
        [Display(Name = "Công thức")]
        public int? CongThucId { get; set; }
        [Display(Name = "Hình ảnh")]
        public string? HinhAnh { get; set; }

        public virtual CongThuc? CongThuc { get; set; }
        public virtual LoaiMon? LoaiMon { get; set; }
        public virtual ICollection<MonDonDatMon> MonDonDatMons { get; set; }
    }
}
