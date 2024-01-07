using DoAn_PTUDWEB.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Controllers
{
	public class AccountController : Controller
	{
		[Route("/Account")]
		public IActionResult Index()
		{
			if (!Functions.IsLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			return View();
		}
	}
}
