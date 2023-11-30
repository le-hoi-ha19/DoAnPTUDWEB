using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Services;
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


builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<ProductService>();







var app = builder.Build();

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

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
