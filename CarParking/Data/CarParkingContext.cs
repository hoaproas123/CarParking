using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarParking.Models;

namespace CarParking.Data
{
    public class CarParkingContext : DbContext
    {
        public CarParkingContext (DbContextOptions<CarParkingContext> options)
            : base(options)
        {
        }

        public DbSet<CarParking.Models.Account> Account { get; set; } = default!;

        public DbSet<CarParking.Models.BaiXe>? BaiXe { get; set; }

        public DbSet<CarParking.Models.KhachHang>? KhachHang { get; set; }

        public DbSet<CarParking.Models.NhanVien>? NhanVien { get; set; }

    }
}
