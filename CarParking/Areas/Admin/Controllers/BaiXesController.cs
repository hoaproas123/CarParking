using CarParking.Areas.Admin.Models;
using CarParking.Data;
using CarParking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarParking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaiXesController : Controller
    {
        private readonly CarParkingContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BaiXesController(CarParkingContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public class Temporary
        {
            public int id { get; set; }
            public BaiXe bx { get; set; }
            public int quantitynow { get; set; }
            public int quantitycheckout { get; set; }
            public int total { get; set; }
        }
        public List<Temporary> tem { get; set; }
        [TempData]
        public string StatusMessage { get; set; }





        public async Task<IActionResult> Index()
        {
            var list = await _context.BaiXe.ToListAsync();
            tem = new List<Temporary>();
            foreach (var item in list)
            {
                var qr_total = (from c in _context.KhachHang where c.BaiXe_Id == item.Id select c.Total).ToList();
                var qr_checkout = (from c in _context.KhachHang where c.BaiXe_Id == item.Id && c.timeOut != null select c);
                var qr_checkin = (from c in _context.KhachHang where c.BaiXe_Id == item.Id && c.timeOut == null select c);
                tem.Add(new Temporary
                {
                    id = item.Id,
                    bx = item,
                    quantitynow = qr_checkin.Count(),
                    quantitycheckout = qr_checkout.Count(),
                    total = qr_total.Sum()
                });
            }
            return View(tem);
        }




        public IActionResult Create()
        {
            //ViewData["BaiXe_Id"] = new SelectList(_context.BaiXe, "Id", "Id"); 
            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DiaChi,AllSlot,Price")] BaiXe BaiXe)
        {
            //if (ModelState.IsValid)
            //{
            BaiXe bx = new BaiXe
            {
                Name = BaiXe.Name,
                DiaChi = BaiXe.DiaChi,
                AllSlot = BaiXe.AllSlot,
                Price = BaiXe.Price 
            };

            _context.Add(bx);
            var result = await _context.SaveChangesAsync();
            if (result != 0)
            {
                StatusMessage = "Tạo bãi xe thành công";
            }
            else
            {
                StatusMessage = "Error tạo thất bại";
                return View();
            }
            return RedirectToAction(nameof(Index));
            //}
            //ViewData["BaiXe_Id"] = new SelectList(_context.BaiXe, "Id", "NhanVien_Id", khachHang.BaiXe_Id);
            //return View(khachHang);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var bx = await _context.BaiXe.FindAsync(id);
            var a =await _userManager.Users.ToListAsync();
            var listStaff = (from c in _userManager.Users where c.BaiXe_Id == bx.Id select c).ToList();
            string listString = "Không có nhân viên";
            
            if (bx == null)
            {
                StatusMessage = "Error thông tìm thấy bãi xe";
                return View("/index");
            }

            if (listStaff != null)
            {
                 
                  listString = "";
                
                TempData["ListStaff"] = listStaff;
            }
            else
            {
                TempData["ListStaff"] = "Bãi này không có nhân viên";
            }

             
            return View(bx);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BaiXe == null)
            {
                return NotFound();
            }

            var bx = await _context.BaiXe.FindAsync(id);
            if (bx == null)
            {
                StatusMessage = "Error bãi xe không tồn tại";
            }

            return View(bx);
        }

        // POST: KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BaiXe == null)
            {
                return Problem("Entity set 'DataContext.KhachHang'  is null.");
            }
            var bx = await _context.BaiXe.FindAsync(id);
            if (bx != null)
            {
                _context.BaiXe.Remove(bx);
                StatusMessage = "Xóa thành công";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.BaiXe.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);


             
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,DiaChi,AllSlot,Price")] BaiXe baixe)
        {
            if (id != baixe.Id)
            {
                return NotFound("khac");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baixe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                { 
                }
                return RedirectToAction(nameof(Index));
            }
            return View(baixe);
        }
    }
}
