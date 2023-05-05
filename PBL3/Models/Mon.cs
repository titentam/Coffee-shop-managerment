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
        public string? TenMon { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập giá")]
        [Display(Name = "Giá")]
		public decimal? Gia { get; set; }
        public int? LoaiMonId { get; set; }
        public int? CongThucId { get; set; }
        public string? HinhAnh { get; set; }

        public virtual CongThuc? CongThuc { get; set; }
        public virtual LoaiMon? LoaiMon { get; set; }
        public virtual ICollection<MonDonDatMon> MonDonDatMons { get; set; }
    }
}
