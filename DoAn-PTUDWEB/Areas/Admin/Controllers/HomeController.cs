using DoAn_PTUDWEB.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
			//// thêm để check đăng nhập và phân quyền
			//if (!Functions.IsLogin())
			//{
			//	return RedirectToAction("Index", "Login");
			//}
			//else if(Functions.CheckAdminPermission())
			//{

			//}
			//else
			//{
			//	return NotFound();
			//}
				return View();

        }
		public IActionResult Logout()
		{
			Functions._UserID = 0;
			Functions._UserName = string.Empty;
			Functions._Message = string.Empty;
			Functions._MessageUserName = string.Empty;
			Functions._FullName = string.Empty;

			return RedirectToAction("Index", "Home");
		}
	}
}
