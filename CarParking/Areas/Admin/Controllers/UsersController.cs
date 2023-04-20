using CarParking.Areas.Admin.Models;
using CarParking.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static CarParking.Areas.Admin.Controllers.RolesController;

namespace CarParking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CarParkingContext _context;
        public class InputModel
        {
            public string ID { set; get; }
            public string Name { set; get; }

            public string[] RoleNames { set; get; }
            public List<string> ListRole { set; get; }

        }
        [TempData]
        public string StatusMessage { get; set; }

        public List<string> AllRoles { set; get; } = new List<string>();
        public UsersController(UserManager<AppUser> userManager, CarParkingContext dataContext, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = dataContext;
            _roleManager = roleManager;
        }

        public List<AppUser> users { get; set; }
        public async Task<IActionResult> Index()
        {
            var staffUser = new AppUser { UserName = "a@a.com", Email = "staff@staff.com" };
            users = await _userManager.Users.ToListAsync();
            foreach (var u in users)
            {   
                var roles = await _userManager.GetRolesAsync(u);
                u.RoleNames = string.Join(",", roles);
            }
            return View(users);
        }


        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> AssignRole(string? userid)
        {
            if (userid == null) { return NotFound("userid null"); }
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null) { return NotFound("ko tim thay user"); }

            var allroles = await _roleManager.Roles.ToListAsync();
            if (allroles == null) { return NotFound("ko lay dc role"); }
            allroles.ForEach(r => AllRoles.Add(r.Name));
            string[] userRoles = (await _userManager.GetRolesAsync(user)).ToArray();

            InputModel inputModel = new InputModel
            {
                ID = user.Id,
                Name = user.UserName,
                RoleNames = userRoles,
                ListRole = AllRoles
            };

            return View(inputModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string Id, [Bind("Name")] InputModel Input, string[] selectedItems)
        {

            if (Id == null) { return NotFound("userid null"); }
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null) { return NotFound("ko tim thay user"); }


            var roles = await _userManager.GetRolesAsync(user);
            var allroles = await _roleManager.Roles.ToListAsync();
            if (allroles == null) { return NotFound("loi"); }

            StatusMessage = $"Vừa cập nhật {user.UserName}";
            if (Input.RoleNames == null) Input.RoleNames = new string[] { };
            foreach (var rolename in roles)
            {
                //if (Input.RoleNames.Contains(rolename)) continue;
                await _userManager.RemoveFromRoleAsync(user, rolename);
            }
            foreach (var rolename in selectedItems)
            {
                if (roles.Contains(rolename)) continue;
                await _userManager.AddToRoleAsync(user, rolename);
            }
            user.RoleNames = "";
            user.RoleNames = string.Join(",", roles);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(string? userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            ViewData["BaiXe_Id"] = new SelectList(_context.BaiXe, "Id", "Id");

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, [Bind("BaiXe_Id")] AppUser appUser)
        {
            var user = await _userManager.FindByIdAsync(Id);
            
            user.BaiXe_Id = appUser.BaiXe_Id;
             
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                StatusMessage = $" Cập nhật thành công";
                return RedirectToAction(nameof(Index));
            }
            StatusMessage = $"Lỗi Cập nhật role thất bại";
            return RedirectToAction(nameof(Index));
        }
    }
}
