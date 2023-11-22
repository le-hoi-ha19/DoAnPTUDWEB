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


		[Route("/Product/Detail/{id:int}", Name = "Detail")]
        public IActionResult Detail(int id)
		{
            if (id <= 0)
            {
                return NotFound();
            }
            var product = _context.TbProducts.FirstOrDefault(m => (m.ProductId == id) && (m.IsActive == true));
            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }

		[Route("/Product/Cart")]
		public IActionResult Cart()
		{
			return View();
		}

		
	}
}
