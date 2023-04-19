using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarParking.Data;
using CarParking.Models;

namespace CarParking.Controllers
{
    public class BaiXesController : Controller
    {
        private readonly DataContext _context;

        public BaiXesController(DataContext context)
        {
            _context = context;
        }

        // GET: BaiXes
        public async Task<IActionResult> Index()
        {
            var carParkingContext = _context.BaiXe.Include(b => b.NhanVien);
            return View(await carParkingContext.ToListAsync());
        }

        // GET: BaiXes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BaiXe == null)
            {
                return NotFound();
            }

            var baiXe = await _context.BaiXe
                .Include(b => b.NhanVien)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baiXe == null)
            {
                return NotFound();
            }

            return View(baiXe);
        }

        // GET: BaiXes/Create
        public IActionResult Create()
        {
            ViewData["NhanVien_Id"] = new SelectList(_context.Set<NhanVien>(), "Id", "Id");
            return View();
        }

        // POST: BaiXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AllSlot,RemainingSlot,NhanVien_Id,Price")] BaiXe baiXe)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(baiXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["NhanVien_Id"] = new SelectList(_context.Set<NhanVien>(), "Id", "Id", baiXe.NhanVien_Id);
            //return View(baiXe);
        }

        // GET: BaiXes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BaiXe == null)
            {
                return NotFound();
            }

            var baiXe = await _context.BaiXe.FindAsync(id);
            if (baiXe == null)
            {
                return NotFound();
            }
            ViewData["NhanVien_Id"] = new SelectList(_context.Set<NhanVien>(), "Id", "Id", baiXe.NhanVien_Id);
            return View(baiXe);
        }

        // POST: BaiXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AllSlot,RemainingSlot,NhanVien_Id,Price")] BaiXe baiXe)
        {
            if (id != baiXe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baiXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaiXeExists(baiXe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NhanVien_Id"] = new SelectList(_context.Set<NhanVien>(), "Id", "Id", baiXe.NhanVien_Id);
            return View(baiXe);
        }

        // GET: BaiXes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BaiXe == null)
            {
                return NotFound();
            }

            var baiXe = await _context.BaiXe
                .Include(b => b.NhanVien)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baiXe == null)
            {
                return NotFound();
            }

            return View(baiXe);
        }

        // POST: BaiXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BaiXe == null)
            {
                return Problem("Entity set 'DataContext.BaiXe'  is null.");
            }
            var baiXe = await _context.BaiXe.FindAsync(id);
            if (baiXe != null)
            {
                _context.BaiXe.Remove(baiXe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiXeExists(int id)
        {
          return (_context.BaiXe?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
