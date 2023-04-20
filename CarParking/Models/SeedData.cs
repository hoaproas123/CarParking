using CarParking.Areas.Admin.Models;
using CarParking.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarParking.Models
{
	public static class SeedData
	{
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        { 

		}
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new CarParkingContext(
			serviceProvider.GetRequiredService<
			DbContextOptions<CarParkingContext>>()))
			{
				// Look for any movies.
				if (context.Account.Any() || context.NhanVien.Any() || context.BaiXe.Any() || context.KhachHang.Any())
				{
					return;
				}

				context.Account.AddRange(
				new Account
				{
					Id = "Admin",
					Password = "Admin",
					Role = "Admin"
				},
				new Account
				{
					Id = "Staff",
					Password = "Staff",
					Role = "Staff"
				}
				);
				context.SaveChanges();
				context.NhanVien.AddRange(
				new NhanVien
				{
					Id = "205051928",
					UserName = "Admin",
					HoTen = "Hồ Ngọc Hòa",
					dChi = "105/12 đường Dương Quãng Hàm, phường 5, quận Gò Vấp, TP.Hồ Chí Minh",
					SDT = "0707622862"
				},
				new NhanVien
				{
					Id = "205051915",
					UserName = "Staff",
					HoTen = "Lâm Thiên Em",
					dChi = "5/5 đường Quang Trung, phường 13, quận Phú Nhuận, TP.Hồ Chí Minh",
					SDT = "0676867885"
				},
				new NhanVien
				{
					Id = "205051233",
					UserName = "Staff",
					HoTen = "Nguyễn Tạ Đạt",
					dChi = "52/5 đường Điện Biên Phủ, phường 13, quận Bình Thạnh, TP.Hồ Chí Minh",
					SDT = "0671215512"
				},
				new NhanVien
				{
					Id = "205043423",
					UserName = "Staff",
					HoTen = "Lê Cao Hiếu",
					dChi = "522/55 đường Nguyễn Oanh, phường 13, quận Gò Vấp, TP.Hồ Chí Minh",
					SDT = "0671212424"
				}
				);
				context.SaveChanges();
				context.Database.OpenConnection();
				context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BaiXe ON");
				context.BaiXe.AddRange(
					new BaiXe
					{
						Id = 1,
						AllSlot = 20,
						RemainingSlot = 20,
						NhanVienId = "205051915",
						Price = 30000
					},
					new BaiXe
					{
						Id = 2,
						AllSlot = 30,
						RemainingSlot = 30,
						NhanVienId = "205051233",
						Price = 20000
					}
				);
				context.SaveChanges();
				context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BaiXe OFF");
				context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.KhachHang ON");
				context.KhachHang.AddRange(
				new KhachHang
				{
					Id = 1,
					MaXe = "99H-5678",
					timeIn = DateTime.Parse("08/18/2018 07:22:16"),
					timeOut = DateTime.Parse("08/19/2018 07:22:16"),
					BaiXe_Id = 1,
					Total = 72000
				},
				new KhachHang
				{
					Id = 2,
					MaXe = "66F-64326",
					timeIn = DateTime.Parse("08/18/2018 07:22:16"),
					timeOut = DateTime.Parse("08/19/2018 07:22:16"),
					BaiXe_Id = 1,
					Total = 90000
				},
				new KhachHang
				{
					Id = 3,
					MaXe = "65U-64126",
					timeIn = DateTime.Parse("08/18/2018 07:22:16"),
					timeOut = DateTime.Parse("08/19/2018 07:22:16"),
					BaiXe_Id = 2,
					Total = 14600000
				},
				new KhachHang
				{
					Id = 4,
					MaXe = "12Y-12453",
					timeIn = DateTime.Parse("08/18/2018 07:22:16"),
					timeOut = DateTime.Parse("08/19/2018 07:22:16"),
					BaiXe_Id = 3,
					Total = 228000
				}
				);
				context.SaveChanges();
				context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.KhachHang OFF");
				context.Database.CloseConnection();
			}
		}
	}

}