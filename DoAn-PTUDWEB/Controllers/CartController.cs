using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Services;
using DoAn_PTUDWEB.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

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
		public IActionResult CartPage()
		{
			// lấy Cart từ session ra để show ra toàn bộ đơn hàng
			Cart = HttpContext.Session.GetJson<Cart>("cart");


			return View("Cart", Cart);
		}

		public class AddToCartRequest
		{
			public int productId { get; set; }
			public int quantity { get; set; } = 1;
			public int colorId { get; set; }
			public int typeId { get; set; }
		}

		// Thêm một đơn hàng hoặc thêm 1 sản phẩm vào giỏ hàng
		[HttpPost]
		[Route("/Cart/AddToCart/")]
		public IActionResult AddToCart([FromBody] AddToCartRequest request)
		{
			TbProduct? product = _context.TbProducts.FirstOrDefault(p => p.ProductId == request.productId);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				var colorName = _context.TbColors.Where(c => c.ColorId == request.colorId).Select(c => c.ColorName).FirstOrDefault();
				var typeName = _context.TbTypes.Where(t => t.TypeId == request.typeId).Select(t => t.TypeName).FirstOrDefault();
				Cart.AddItem(product, request.quantity, request.colorId, request.typeId, colorName, typeName);

				HttpContext.Session.SetJson("cart", Cart);

			}
			return Ok();
		}

		//  xóa một mặt trong đơn hàng
		[HttpPost]
		[Route("/Cart/UpdateCart/")]
		public IActionResult UpdateCart([FromBody] AddToCartRequest request)
		{
			TbProduct? product = _context.TbProducts.FirstOrDefault(p => p.ProductId == request.productId);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				var colorName = _context.TbColors.Where(c => c.ColorId == request.colorId).Select(c => c.ColorName).FirstOrDefault();
				var typeName = _context.TbTypes.Where(t => t.TypeId == request.typeId).Select(t => t.TypeName).FirstOrDefault();
				Cart.AddItem(product, -1, request.colorId, request.typeId, colorName, typeName);
				HttpContext.Session.SetJson("cart", Cart);

			}
			return Ok();
		}

		// xóa 1 đơn hàng
		//[HttpDelete]
		[Route("/Cart/RemoveFromCart/")]
		public IActionResult RemoveFromCart([FromBody] AddToCartRequest request)
		{
			TbProduct? product = _context.TbProducts.FirstOrDefault(p => p.ProductId == request.productId);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart");
				Cart.RemoveLine(product, request.colorId, request.typeId);
				HttpContext.Session.SetJson("cart", Cart);

			}
			return Ok();
		}

        // hiển thị trang nhập thông tin đơn hàng
        [Route("Cart/fillform")]
        public IActionResult fillform()
        {
			var Users = _context.TbUsers.ToList();
			ViewBag.Users = Users;
            return View("FormOrder");
        }



        // xử lý khi người dùng đặt đơn hàng
        [HttpPost]
        [Route("/Cart/handlecart")]
        public async Task<IActionResult> handlecart(TbOrder tbOrder)
        {
             _context.TbOrders.Add(tbOrder);
			await _context.SaveChangesAsync();


			int IdOrder = tbOrder.OrderId;
            Cart = HttpContext.Session.GetJson<Cart>("cart");
			var ListCartLine = Cart?.Lines;
			if (ListCartLine != null && Cart != null && IdOrder >= 0)
			{
                foreach (var cartLine in ListCartLine)
                {
                    var inforDetail = new TbOrderDetail();
                    inforDetail.OrderId = IdOrder;
                    inforDetail.ProductId = cartLine.Product.ProductId;
                    inforDetail.Quantity = cartLine.Quantity;
					inforDetail.ColorId = cartLine.ColorId;
					inforDetail.TypeId = cartLine.TypeId;
                    inforDetail.Price = (cartLine.Product.Price -(cartLine.Product.Price * cartLine.Product.PriceDiscount));
					inforDetail.TotalMoney =(cartLine.Quantity * (cartLine.Product.Price - (cartLine.Product.Price * cartLine.Product.PriceDiscount)));
                    _context.TbOrderDetails.Add(inforDetail);
					await _context.SaveChangesAsync();
				}

            }

            HttpContext.Session.RemoveSession("cart");
			return RedirectToAction("Index", "Home");
		}

    }
}
