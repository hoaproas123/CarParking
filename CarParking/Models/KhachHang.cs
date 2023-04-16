using System.ComponentModel.DataAnnotations;

namespace CarParking.Models
{
    public class KhachHang
    {
        [Key] 
        public string Id { get; set; }
        [Required]
        public DateTime timeIn{ get; set; }
        [Required]
        public DateTime timeOut { get; set; }
        [Required]
        public virtual BaiXe BaiXe_Id { get; set; }
        [Required]
        public decimal Total { get; set; }

    }
}
