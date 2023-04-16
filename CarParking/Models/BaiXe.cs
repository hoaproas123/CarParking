using System.ComponentModel.DataAnnotations;

namespace CarParking.Models
{
    public class BaiXe
    {
        [Key]
        public int Id { get; set; }
        [Required] 
        public int AllSlot { get; set; }
        [Required]
        public int RemainingSlot { get; set; }
        [Required]
        public virtual NhanVien NhanVienQL_ID { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
