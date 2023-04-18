
using System.ComponentModel.DataAnnotations;
namespace CarParking.Models
{
    public class Account
    {
        [Key]
		[Display(Name = "Tài Khoản")]
		public string Id { get; set; }
        [Required]
		[Display(Name = "Mật Khẩu")]
		public string Password { get; set; }
        [Required]
		[Display(Name = "Quyền Hạn")]
		public string Role { get; set; }
    }
}
