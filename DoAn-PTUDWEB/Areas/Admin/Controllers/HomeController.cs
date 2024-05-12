using DoAn_PTUDWEB.Constains;
using DoAn_PTUDWEB.Infrastructure;
using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Models.ViewModels;
using DoAn_PTUDWEB.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAn_PTUDWEB.Filters;
//using System.Web.Security;

namespace DoAn_PTUDWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize]
	[AdminRequired]
	public class HomeController : Controller
    {
		private readonly DataContext _context;

		public HomeController(DataContext context)
		{
			_context = context;
		}

		//public bool checkPermission()
		//{

		//	var currentUser = HttpContext.Session.GetJson<UserViewModel>("taiKhoan");
		//	// có thể thay RoleId để nó thay quyền
		//	var count = _context.TbUsers.Count(m => m.UserId == currentUser.UserId && m.RoleId == 1);
		//	if (count == 0)
		//	{
		//		return false;

		//	}
		//	else
		//	{
		//		return true;
		//	}
		//}
		public IActionResult Index()
        {
			//if (HttpContext.Session.GetJson<UserViewModel>("taiKhoan") == null)
			//{
			//	return Redirect("/Login");
			//}

			//if(checkPermission() == false)
			//{
			//	return Redirect("/NotPermission/Index");
			//}
			return View();
        }
    }
}
