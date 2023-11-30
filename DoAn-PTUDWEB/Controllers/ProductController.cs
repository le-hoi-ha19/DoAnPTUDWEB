using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace DoAn_PTUDWEB.Controllers
{
	public class ProductController : Controller
	{
        private readonly ILogger<ProductController> _logger;
        private readonly DataContext _context;

		private readonly ReviewService _reviewService;
		private readonly ProductService _productService;

		public ProductController(ILogger<ProductController> logger, DataContext context, ReviewService reviewService, ProductService productService)
        {
            _logger = logger;
            _context = context;
			_reviewService = reviewService;
			_productService = productService;

		}


		[Route("/Product/Detail/{ProductId:int}", Name = "Detail")]
		public IActionResult Detail(int ProductId , int rating =0)
		{
			if (ProductId <= 0)
            {
                return NotFound();
            }
			// truy vấn sản phẩm theo id
			var product = _productService.GetProductById(ProductId);

			if (product == null)
			{
				return NotFound();
			}
			//truy vấn các ảnh khác của sản phẩm
			var imagesProduct = _productService.GetImagesForProduct(ProductId);

			// truy vấn các màu của 1 sản phẩm
			var colorProduct = _productService.GetColorsForProduct(ProductId);

			// truy vấn sản phẩm liên quan và k cho sản phẩm liên quan hiển thị sản phẩm đang xem
			var relatedProduct = _productService.GetRelatedProducts(ProductId);


			if (rating == 0)
			{
				// truy vấn lấy ra tất cả các đánh giá của 1 sản phẩm
				var AllReview = _reviewService.GetAllReview(ProductId);
				ViewBag.AllReview = AllReview;
			}


			// thông tin chi tiết của 1 sản phẩm
			var inforDetail = new ProductDetail()
			{
				Product = product,
				Images = imagesProduct,
				Colors = colorProduct,
				relatedProducts = relatedProduct,
			};

			

			return View(inforDetail);
		}

		[HttpGet]
		[Route("/Product/Reviews")]
		public IActionResult Review(int ProductId ,int? rating)
		{
			var Product = _productService.GetProductById(ProductId);
			if (Product == null)
			{
				return NotFound();
			}

			if(!rating.HasValue)
			{
				var AllReview = _reviewService.GetAllReview(ProductId);
				return Ok(AllReview);
			}
			var ReviewByStar = _reviewService.GetReviewByStar(ProductId,rating);

			return Ok(ReviewByStar);
		}

		[Route("/Product/Cart")]
		public IActionResult Cart()
		{
			return View();
		}

		
	}
}
