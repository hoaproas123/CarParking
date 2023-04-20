using Microsoft.AspNetCore.Identity;

namespace CarParking.Areas.Admin.Models
{
    public class AppUser : IdentityUser
    {
        public string? RoleNames { get; set; }
        public int? BaiXe_Id { get; set; }

        public string? Ten { get; set; }
    }
}
