
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace CarParking.Models
{
    public class NhanVien
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public virtual Account UserName { get; set; }
        [Required]
        public string HoTen { get; set; }
        [Required]
        public string SDT { get; set; }
        [Required]
        public string dChi { get; set; }        
    }
}
