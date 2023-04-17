
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace CarParking.Models
{
    public class NhanVien
    {
        [Key]
        public string Id { get; set; }
        public string? UserName { get; set; }
        [ForeignKey("UserName")]
        public virtual Account Account { get; set; }
        [Required]
        public string HoTen { get; set; }
        [Required]
        public string SDT { get; set; }
        [Required]
        public string dChi { get; set; }        
    }
}
