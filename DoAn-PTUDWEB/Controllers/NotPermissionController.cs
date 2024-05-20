using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Controllers
{
	public class NotPermissionController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
