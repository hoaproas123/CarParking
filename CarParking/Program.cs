using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarParking.Data;
using CarParking.Models;
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Identity.UI.Services;
using CarParking.Areas.Admin.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CarParkingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarParkingContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<DataContext>();
// Add services to the container.
//builder.Services.AddDefaultIdentity<IdentityUser>();
builder.Services.AddIdentity<AppUser, IdentityRole>( )
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarParkingContext>();
//builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
//builder.Services.AddTransient<IEmailSender>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt
});
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();







var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();
 
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



});
app.Run();
