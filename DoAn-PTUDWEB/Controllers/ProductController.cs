using DoAn_PTUDWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Controllers
{
	public class ProductController : Controller
	{
        private readonly ILogger<ProductController> _logger;
        private readonly DataContext _context;

        public ProductController(ILogger<ProductController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }


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
