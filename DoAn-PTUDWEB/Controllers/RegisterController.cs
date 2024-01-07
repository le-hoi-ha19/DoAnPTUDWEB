using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Controllers
{
    public class RegisterController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		private readonly DataContext _context;
		public RegisterController(ILogger<HomeController> logger, DataContext context)
		{
			_logger = logger;
			_context = context;
		}
		[HttpGet]
		[Route("/Register")]
		public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		[Route("/Register")]
		public IActionResult Index(TbUser user)
		{
			if(user == null)
			{
				return NotFound();
			}
			// kiểm tra sự tồn tại của tên tài khoản(username) trong CSDL
			var CheckUserName = _context.TbUsers.Where(m => m.UserName == user.UserName).FirstOrDefault();
			if (CheckUserName != null)
			{
				// Hiển thị thông báo user đã được đăng ký
				Functions._MessageUserName = "Tên tài khoản đã được đăng ký!";
				return RedirectToAction("Index", "Register");
			}
			Functions._MessageUserName = string.Empty;

			// nếu k trùng Tên tài khoản thì thêm người dùng vào CSDL
			user.PasswordHash = Functions.MD5Password(user.PasswordHash);
			_context.TbUsers.Add(user);
			_context.SaveChanges();
            return RedirectToAction("Index", "Login");

        }
    }
}
