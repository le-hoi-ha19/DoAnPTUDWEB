using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Utilities;
using Microsoft.AspNetCore.Mvc;

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
		// khi người dùng bấm nút đăng nhập thì nhảy vào đây để xử lý
		[HttpPost]
		[Route("/Login")]
		public IActionResult Index(TbUser user)
		{

			if (user == null)
			{
				return NotFound();
			}

			// Mã hóa mật khẩu trước khi kiểm tra
			string pw = Functions.MD5Password(user.PasswordHash);
			var check = _context.TbUsers.Where(m => (m.UserName == user.UserName) && (m.PasswordHash == pw)).FirstOrDefault();
			if (check == null)
			{
				// Hiển thị thông báo có thể làm cách khác
				Functions._Message = "Tài khoản hoặc mật khẩu không đúng!";
				return RedirectToAction("Index", "Login");
			}
			// lưu thông tin người dùng đăng nhập hiện tại vào biến để hiển thị ra giao diện của admin và check đăng nhập
			Functions._Message = string.Empty;
			Functions._UserID = check.UserId;
			Functions._UserName = string.IsNullOrEmpty(check.UserName) ? string.Empty : check.UserName;
			Functions._FullName = string.IsNullOrEmpty(check.FullName) ? "user" : check.FullName;
			Functions._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
			Functions._Address = string.IsNullOrEmpty(check.Address) ? string.Empty : check.Address;
			Functions._RoleId = (int)check.RoleId;

			var IsCheckPermission = _context.TbUsers.FirstOrDefault(m => m.UserName == user.UserName && m.RoleId == 2);

			if (IsCheckPermission != null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return RedirectToAction("Index", "Admin");
			}

		}
	}
}
