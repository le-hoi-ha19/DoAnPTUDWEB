using DoAn_PTUDWEB.Infrastructure;
using DoAn_PTUDWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Components
{
	[ViewComponent(Name = "CartWidget")]

	public class CartWidgetComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View(HttpContext.Session.GetJson<Cart>("cart"));
		}

	}
}
