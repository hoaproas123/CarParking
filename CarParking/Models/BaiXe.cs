using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarParking.Models
{
    public class BaiXe
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên bãi")]
        public string? Name { get; set; }
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }
         

        [Required]
        [Display(Name = "Tổng Slot")]
        public int AllSlot { get; set; }
        [Required]
        [Display(Name = "Giá Vào Bãi")]
        public int Price { get; set; }
        [Required]
        [Display(Name = "Slot Còn")]
        public int RemainingSlot { get; set; }
        public string? NhanVienId { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
