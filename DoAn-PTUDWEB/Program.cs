using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// lấy chuỗi kết nối từ file appsetting.json
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
// truyền chuỗi kết nối vào tham số option của contructor DbContext
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
// Add services to the container.
builder.Services.AddControllersWithViews();
// Add Razor cho biên dịch lại html trong quá trình chạy
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
// Add services to access HttpContext from custom service
//thêm để xử lý phần active sidebar
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
	options.LoginPath = "/Login";
	options.AccessDeniedPath = "/NotPermission/Index";
});


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddSingleton<IVnPayService, VnPayService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



//thêm phần admin
app.MapControllerRoute(
	name: "areas",
	pattern: "{Area:exists}/{controller=Home}/{action=Index}/{id?}"
		);


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

app.Run();
