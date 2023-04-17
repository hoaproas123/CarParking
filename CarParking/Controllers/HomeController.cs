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
        private readonly CarParkingContext _context;

        public HomeController(ILogger<HomeController> logger, CarParkingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["BaiXe_Id"] = new SelectList(_context.BaiXe, "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn([Bind("Id,MaXe,timeIn,timeOut,BaiXe_Id,Total")] KhachHang khachHang)
        {
            BaiXe BaiXe = _context.BaiXe.Where(x => x.Id == khachHang.BaiXe_Id).FirstOrDefault();
            khachHang.timeIn = DateTime.Now;
            khachHang.timeOut = null;
            khachHang.Total = BaiXe.Price;
            BaiXe.RemainingSlot--;
            _context.Add(khachHang);
            _context.Update(BaiXe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(string? MaXe)
        {
            KhachHang khachHang = _context.KhachHang.Where(x => x.MaXe == MaXe && x.timeOut==null).FirstOrDefault();
            BaiXe BaiXe = _context.BaiXe.Where(x => x.Id == khachHang.BaiXe_Id).FirstOrDefault();
            khachHang.timeOut = DateTime.Now;
            khachHang.Total = BaiXe.Price +TinhTien(khachHang.timeIn,khachHang.timeOut);
            BaiXe.RemainingSlot++;
            _context.Update(khachHang);
            _context.Update(BaiXe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CheckedIn()
        {
            var carParkingContext = _context.KhachHang.Include(k => k.BaiXe).Where(k=>k.timeOut==null);
            return View(await carParkingContext.ToListAsync());
        }
        public async Task<IActionResult> CheckedOut()
        {
            var carParkingContext = _context.KhachHang.Include(k => k.BaiXe).Where(k=>k.timeOut!=null);
            return View(await carParkingContext.ToListAsync());
        }
        public int TinhTien(DateTime a,DateTime? b)
        {
            int TotalTime = (b.Value.Month-a.Month)*43200+(b.Value.Day-a.Day)*1440+(b.Value.Hour-a.Hour)*60+(b.Value.Minute-a.Minute);
            return TotalTime/60*20000;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}