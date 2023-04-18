
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace CarParking.Models
{
    public class NhanVien
    {
        [Key]
        public string Id { get; set; }
		[Display(Name = "Tên Đăng Nhập")]
		public string? UserName { get; set; }
        [ForeignKey("UserName")]
        public virtual Account Account { get; set; }
        [Required]
		[Display(Name = "Họ Tên")]
		public string HoTen { get; set; }
        [Required]
		[Display(Name = "Số Điện Thoại")]
		public string SDT { get; set; }
        [Required]
		[Display(Name = "Địa Chỉ")]
		public string dChi { get; set; }        
    }
}
