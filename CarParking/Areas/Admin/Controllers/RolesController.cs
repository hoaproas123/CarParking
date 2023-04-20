using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CarParking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CarParking.Areas.Admin.Controllers
{ 
    [Area("Admin")]
    public class RolesController : Controller
    {
        public class RoleInput
        {
            [DisplayName("Tên của role")]
            [Required(ErrorMessage = "Title is required.")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài từ {2} cho đến {1} kí tự")]
            public string Name { get; set; }
        }
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CarParkingContext _dataContext;
        [TempData]
        public string StatusMessage { get; set; }
        public RolesController(RoleManager<IdentityRole> roleManager, CarParkingContext dataContext)
        {
            _roleManager = roleManager;
            _dataContext = dataContext;
        }

        public List<IdentityRole> roles { get; set; }

        public async Task<IActionResult> Index()
        {
            roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }

        public async Task<IActionResult> CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole([Bind("Name")] RoleInput roleInput)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }



            var newRole = new IdentityRole(roleInput.Name);
            var result = await _roleManager.CreateAsync(newRole);

            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa tạo một role mới: {newRole.Name}";
                return RedirectToAction("Index");
            }
            else
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }
            return View();



        }




        // GET: ParkingLots/Edit/5
        public async Task<IActionResult> Edit(string? roleid)
        {
            if (roleid == null)
            {
                return NotFound("Không tìm thấy role");
            }

            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                return NotFound("Không tìm thấy role");
            }

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, [Bind("Name")] RoleInput roleInput)
        {

            if (Id == null)
            {
                return NotFound("Không tìm thấy roleid");
            }
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return NotFound("Không tìm thấy role");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            role.Name = roleInput.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                StatusMessage = $"Cập nhật thành công role {role.Name}";
                return RedirectToAction(nameof(Index));
            }
            StatusMessage = $"Cập nhật role thất bại";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Test()
        {
            var role = await _roleManager.FindByIdAsync("2c6ab641-ba34-456b-801a-0280f5163e86");
            return View(role);
        }

        public async Task<IActionResult> DeleteRole(string? roleid)
        {
            if (roleid == null) { return NotFound("tham so roleid null"); }
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null) { return NotFound("role null"); }
            return View(role);
        }

        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoleConfirmed(string Id)
        {
            if (Id == null) { return NotFound("tham so roleid null"); }
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null) { return NotFound($"role null {Id}"); }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                StatusMessage = "Xoa role thanh cong";
                return RedirectToAction(nameof(Index));
            }
            StatusMessage = "Xoa role that bai";
            return RedirectToAction(nameof(Index));
        }

    }
}
