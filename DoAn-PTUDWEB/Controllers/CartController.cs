using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Services;
using DoAn_PTUDWEB.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DoAn_PTUDWEB.Models.ViewModels;
using Newtonsoft.Json;

namespace DoAn_PTUDWEB.Controllers
{
    public class CartController : Controller
	{
		private readonly ILogger<ProductController> _logger;
		private readonly DataContext _context;
		private readonly ProductService _productService;
        private IVnPayService _vnPayservice;
		public Cart? Cart { get; set; }

        public CartController(ILogger<ProductController> logger, DataContext context, ProductService productService, IVnPayService vnPayservice)
		{
			_logger = logger;
			_context = context;
			_productService = productService;
            _vnPayservice = vnPayservice;


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
        [Authorize]
        public IActionResult fillform()
        {
            var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var id = !string.IsNullOrEmpty(idString) ? int.Parse(idString) : (int?)null;
            var Users = _context.TbUsers.FirstOrDefault(m =>m.UserId == id);
			ViewBag.Users = Users;
            Cart = HttpContext.Session.GetJson<Cart>("cart");
			ViewBag.Cart = Cart;
            return View("FormOrder");
        }


		// xử lý khi người dùng đặt đơn hàng
		[HttpPost]
        [Authorize]
        [Route("/Cart/handlecart")]
        public async Task<IActionResult> handlecart(TbOrder tbOrder, string payment= "COD")
        {
			var User = _context.TbUsers.FirstOrDefault(m=>m.UserId == tbOrder.UserId);
			Cart = HttpContext.Session.GetJson<Cart>("cart");
			TempData["tbOrder"] = JsonConvert.SerializeObject(tbOrder);
			tbOrder.IsPayment = false;
			if (payment == "Thanh toán VNPay")
				{
				var vnPayModel = new VnPaymentRequestModel
				{
					Amount = Cart.ComputeTotalValue(),
					CreatedDate = DateTime.Now,
					Description = $"{User.FullName} {User.Phone}",
					FullName = User.FullName,
					OrderId = new Random().Next(1000, 100000)
				};
					return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
				}

			_context.TbOrders.Add(tbOrder);
			await _context.SaveChangesAsync();

			
				int IdOrder = tbOrder.OrderId;
				var ListCartLine = Cart?.Lines;
				if (ListCartLine != null && Cart != null && IdOrder >= 0)
				{
					foreach (var cartLine in ListCartLine)
					{
						var inforDetail = new TbOrderDetail();
						inforDetail.OrderId = IdOrder;
						inforDetail.ProductId = cartLine.Product.ProductId;
						inforDetail.Quantity = cartLine.Quantity;
						var Product = _context.TbProducts.FirstOrDefault(p => p.ProductId == inforDetail.ProductId);
						Product.Quantity -= inforDetail.Quantity;
						inforDetail.ColorId = cartLine.ColorId;
						inforDetail.TypeId = cartLine.TypeId;
						inforDetail.Price = (cartLine.Product.Price - (cartLine.Product.Price * cartLine.Product.PriceDiscount));
						inforDetail.TotalMoney = (cartLine.Quantity * (cartLine.Product.Price - (cartLine.Product.Price * cartLine.Product.PriceDiscount)));
						_context.TbOrderDetails.Add(inforDetail);
						await _context.SaveChangesAsync();
					}

				}

			



			HttpContext.Session.RemoveSession("cart");
			return RedirectToAction("Index", "Home");
		}

        [Authorize]
        public IActionResult PaymentSuccess()
        {
            return View("Success");
        }

        [Authorize]
        public IActionResult PaymentFail()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> PaymentCallBackAsync()
        {
			var response = _vnPayservice.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }

			// Lưu đơn hàng vô database
			Cart = HttpContext.Session.GetJson<Cart>("cart");
			var tbOrder1 = TempData["tbOrder"] as string;
			var tbOrder = JsonConvert.DeserializeObject<TbOrder>(tbOrder1);
			tbOrder.IsPayment = true;
			_context.TbOrders.Add(tbOrder);
			await _context.SaveChangesAsync();

			
				int IdOrder = tbOrder.OrderId;
				var ListCartLine = Cart?.Lines;
				if (ListCartLine != null && Cart != null && IdOrder >= 0)
				{
					foreach (var cartLine in ListCartLine)
					{
						var inforDetail = new TbOrderDetail();
						inforDetail.OrderId = IdOrder;
						inforDetail.ProductId = cartLine.Product.ProductId;
						inforDetail.Quantity = cartLine.Quantity;
						var Product = _context.TbProducts.FirstOrDefault(p => p.ProductId == inforDetail.ProductId);
						Product.Quantity -= inforDetail.Quantity;
						inforDetail.ColorId = cartLine.ColorId;
						inforDetail.TypeId = cartLine.TypeId;
						inforDetail.Price = (cartLine.Product.Price - (cartLine.Product.Price * cartLine.Product.PriceDiscount));
						inforDetail.TotalMoney = (cartLine.Quantity * (cartLine.Product.Price - (cartLine.Product.Price * cartLine.Product.PriceDiscount)));
						_context.TbOrderDetails.Add(inforDetail);
						await _context.SaveChangesAsync();
					}

				}

			HttpContext.Session.RemoveSession("cart");
			TempData["Message"] = $"Thanh toán thành công";
			return RedirectToAction("PaymentSuccess");
        }

    }
}
