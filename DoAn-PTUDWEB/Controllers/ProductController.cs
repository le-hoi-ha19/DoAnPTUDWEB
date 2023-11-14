using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Controllers
{
	public class ProductController : Controller
	{

        [Route("/Product/Detail")]
        public IActionResult Detail()
		{
			return View();
		}

		[Route("/Product/Cart")]
		public IActionResult Cart()
		{
			return View();
		}

		
	}
}
