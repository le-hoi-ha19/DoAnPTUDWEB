using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Services;
using DoAn_PTUDWEB.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace DoAn_PTUDWEB.Controllers
{
	public class CartController : Controller
	{
		private readonly ILogger<ProductController> _logger;
		private readonly DataContext _context;
		public Cart? Cart { get; set; }


		private readonly ProductService _productService;

		public CartController(ILogger<ProductController> logger, DataContext context, ProductService productService)
		{
			_logger = logger;
			_context = context;
			_productService = productService;

		}
		[Route("/Cart")]
		public IActionResult Index()
		{
			// lấy Cart từ session ra để show ra toàn bộ đơn hàng
			Cart = HttpContext.Session.GetJson<Cart>("cart");

            return View("Cart", Cart);
		}
		// Thêm một đơn hàng hoặc thêm 1 sản phẩm vào giỏ hàng
		[Route("/Cart/AddToCart/{productId:int}")]
		public IActionResult AddToCart(int productId)
		{
			TbProduct? product = _context.TbProducts.FirstOrDefault(p => p.ProductId == productId);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, 1);
				HttpContext.Session.SetJson("cart", Cart);

			}
			return View("Cart", Cart);
		}

		//  xóa một mặt trong đơn hàng
		[Route("/Cart/UpdateCart/{productId:int}")]
		public IActionResult UpdateCart(int productId)
		{
			TbProduct? product = _context.TbProducts.FirstOrDefault(p => p.ProductId == productId);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, -1);
				HttpContext.Session.SetJson("cart", Cart);

			}
			return View("Cart", Cart);
		}

		// xóa 1 đơn hàng
		[Route("/Cart/RemoveFromCart/{productId:int}")]
		public IActionResult RemoveFromCart(int productId)
		{
			TbProduct? product = _context.TbProducts.FirstOrDefault(p => p.ProductId == productId);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart");
				Cart.RemoveLine(product);
				HttpContext.Session.SetJson("cart", Cart);

			}
			return View("Cart", Cart);
		}
	}
}
