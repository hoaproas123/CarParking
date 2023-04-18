using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarParking.Models
{
    public class BaiXe
    {
        [Key]
        public int Id { get; set; }
        [Required]
		[Display(Name = "Tổng Slot")]
		public int AllSlot { get; set; }
        [Required]
		[Display(Name = "Slot Còn")]
		public int RemainingSlot { get; set; }
        [Required]
		[Display(Name = "Mã Nhân Viên Quản Lý Bãi")]
		public string NhanVien_Id { get; set; } 
        [ForeignKey("NhanVien_Id")]
        public virtual NhanVien NhanVien { get; set; }
        [Required]
		[Display(Name = "Giá Vào Bãi")]
		public int Price { get; set; }
    }
}
