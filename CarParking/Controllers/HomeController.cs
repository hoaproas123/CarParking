using CarParking.Data;
using CarParking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarParking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
		[TempData]
		public string StatusMessage { get; set; }

		public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HomePage()
        {
			ViewData["BaiXe_Id"] = new SelectList(_context.BaiXe, "Id", "Id");
			return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn([Bind("Id,MaXe,timeIn,timeOut,BaiXe_Id,Total")] KhachHang khachHang)
        {
            int count_KH = _context.KhachHang.Where(x=>x.MaXe==khachHang.MaXe && x.timeOut==null).Count();
            BaiXe BaiXe = _context.BaiXe.Where(x => x.Id == khachHang.BaiXe_Id).FirstOrDefault();
            if (count_KH==0&&khachHang.MaXe!=null)
            {
                khachHang.timeIn = DateTime.Now;
                khachHang.timeOut = null;
                khachHang.Total = BaiXe.Price;
                BaiXe.RemainingSlot--;
                _context.Add(khachHang);
                _context.Update(BaiXe);
                await _context.SaveChangesAsync();
                StatusMessage = $"Thêm Thành Công Xe: {khachHang.MaXe}";
                return RedirectToAction("HomePage");
			}
            StatusMessage = "Lỗi: Thông tin không hợp lệ hoặc đã tồn tại";
			return RedirectToAction("HomePage");
		}
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(string? MaXe)
        {
            KhachHang khachHang = _context.KhachHang.Where(x => x.MaXe == MaXe && x.timeOut==null).FirstOrDefault();
            if(khachHang!=null)
            {
				BaiXe BaiXe = _context.BaiXe.Where(x => x.Id == khachHang.BaiXe_Id).FirstOrDefault();
				khachHang.timeOut = DateTime.Now;
				khachHang.Total = BaiXe.Price + TinhTien(khachHang.timeIn, khachHang.timeOut, BaiXe.Price);
				BaiXe.RemainingSlot++;
				_context.Update(khachHang);
				_context.Update(BaiXe);
				await _context.SaveChangesAsync();
				StatusMessage = $"Tổng Tiền: {khachHang.Total}";
				return RedirectToAction("HomePage");
			}
			StatusMessage = "Lỗi: Biển Số Xe không tồn tại trong bãi";
			return RedirectToAction("HomePage");
        }
        public async Task<IActionResult> CheckedIn()
        {
			var carParkingContext = _context.KhachHang.Include(k => k.BaiXe).Where(k => k.timeOut == null).OrderByDescending(k => k.timeIn);
			return View(await carParkingContext.ToListAsync());

		}
        public async Task<IActionResult> CheckedOut()
        {
            var carParkingContext = _context.KhachHang.Include(k => k.BaiXe).Where(k=>k.timeOut!=null).OrderByDescending(k=>k.timeOut);
            return View(await carParkingContext.ToListAsync());
        }
        public int TinhTien(DateTime a,DateTime? b,int Gia)
        {
            TimeSpan TotalTime = b.Value-a;
            return (int)TotalTime.TotalHours*Gia;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}