using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarParking.Models
{
    public class KhachHang
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string MaXe { get; set; }
        [Required]
        public DateTime timeIn{ get; set; }
        public DateTime? timeOut { get; set; }
        [Required]
        public int BaiXe_Id { get; set; }
        [ForeignKey("BaiXe_Id")]
        public virtual BaiXe BaiXe { get; set; }
        [Required]
        public int Total { get; set; }

    }
}
