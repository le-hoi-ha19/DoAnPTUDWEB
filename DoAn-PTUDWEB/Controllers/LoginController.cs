using DoAn_PTUDWEB.Infrastructure;
using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Models.ViewModels;
using DoAn_PTUDWEB.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoAn_PTUDWEB.Controllers
{
	public class LoginController : Controller
    {
		private readonly DataContext _context;
		public LoginController(DataContext context)
		{
			_context = context;
		}
		[HttpGet]
		[Route("/Login")]
		public IActionResult Index()
        {
            return View();
        }
	

		[HttpPost]
		[Route("/Login")]
		public IActionResult Index(UserViewModel user)
		{

			if (user == null)
			{
				return NotFound();
			}

			// Mã hóa mật khẩu trước khi kiểm tra
			string pw = Functions.MD5Password(user.PasswordHash);
			var taiKhoan = _context.TbUsers.FirstOrDefault(m => (m.UserName.ToLower() == user.UserName.ToLower()) && (m.PasswordHash == pw));
			if (taiKhoan == null)
			{
				// Hiển thị thông báo có thể làm cách khác
				TempData["error"] = "Tài khoản hoặc mật khẩu không đúng!";
				return RedirectToAction("Index", "Login");
			}
			if(taiKhoan.Status == false)
			{
                // Hiển thị thông báo có thể làm cách khác
                TempData["error"] = "Tài khoản đang bị khóa vui lòng liên hệ quản trị viên!";
                return RedirectToAction("Index", "Login");
            }
			var role = _context.TbRoles.FirstOrDefault(r => r.RoleId == taiKhoan.RoleId);
			if (role == null)
			{
				TempData["error"] = "Tài khoản không hợp lệ";
				return RedirectToAction("Index", "Login");
			}
			// lưu thông tin người dùng đăng nhập hiện tại vào cookies
			var claims = new List<Claim> {
			new Claim(ClaimTypes.Name, taiKhoan.UserName),
			new Claim(ClaimTypes.Email, taiKhoan.Email),
			new Claim(ClaimTypes.NameIdentifier, taiKhoan.UserId.ToString()),
			new Claim(ClaimTypes.Role, role.RoleName)
			};
			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
			HttpContext.SignInAsync(claimsPrincipal);
            return RedirectToAction("Index", "Home");
        }
    }
}
