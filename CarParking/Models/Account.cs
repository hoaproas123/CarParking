
using System.ComponentModel.DataAnnotations;
namespace CarParking.Models
{
    public class Account
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
