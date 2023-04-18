using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarParking.Models
{
    public class KhachHang
    {
        [Key]
        public int Id { get; set; }
        [Required]
		[Display(Name = "Biển Số Xe")]
		public string MaXe { get; set; }
        [Required]
		[Display(Name = "Giờ Vào")]
		public DateTime timeIn{ get; set; }
		[Display(Name = "Giờ Ra")]
		public DateTime? timeOut { get; set; }
        [Required]
		[Display(Name = "Mã Bãi Xe")]
		public int BaiXe_Id { get; set; }
        [ForeignKey("BaiXe_Id")]
        public virtual BaiXe BaiXe { get; set; }
        [Required]
		[Display(Name = "Tổng Tiền")]
		public int Total { get; set; }

    }
}
