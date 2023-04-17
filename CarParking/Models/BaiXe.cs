using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string NhanVien_Id { get; set; } 
        [ForeignKey("NhanVien_Id")]
        public virtual NhanVien NhanVien { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
