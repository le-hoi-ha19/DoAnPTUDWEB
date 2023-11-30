using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace DoAn_PTUDWEB.Controllers
{
	public class ProductController : Controller
	{
        private readonly ILogger<ProductController> _logger;
        private readonly DataContext _context;

		private readonly ReviewService _reviewService;

        public ProductController(ILogger<ProductController> logger, DataContext context, ReviewService reviewService)
        {
            _logger = logger;
            _context = context;
			_reviewService = reviewService;
        }


		[Route("/Product/Detail/{ProductId:int}", Name = "Detail")]
        public IActionResult Detail(int ProductId , int? rating = 0)
		{
			// Lưu giá trị vào session
			HttpContext.Session.SetInt32("CurrentProductId", ProductId);

			if (ProductId <= 0)
            {
                return NotFound();
            }
			// truy vấn sản phẩm theo id
			var product = _context.TbProducts.FirstOrDefault(m => (m.ProductId == ProductId) && (m.IsActive == true));

			if (product == null)
			{
				return NotFound();
			}

			//truy vấn các ảnh khác của sản phẩm
			var imagesProduct = _context.TbImageProducts.Where(m => (m.ProductId == ProductId)).Take(4).ToList();

			// truy vấn các màu của 1 sản phẩm
			var colorProduct = (from ProductColor in _context.TbProductColors
								join Product1 in _context.TbProducts on ProductColor.ProductId equals Product1.ProductId
								join Color in _context.TbColors on ProductColor.ColorId equals Color.ColorId
								where Product1.ProductId == ProductId
								select new TbColor
								{
									ColorName = Color.ColorName
								}).ToList();

			// truy vấn sản phẩm liên quan và k cho sản phẩm liên quan hiển thị sản phẩm đang xem
			var relatedProduct = _context.TbProducts.Where(m => m.IsActive == true  && m.ProductId != ProductId && _context.TbProducts
					.Where(p => p.ProductId == ProductId)
					.Select(p => p.CategoryProductId)
					.Contains(m.CategoryProductId))
			.OrderByDescending(m => m.CreatedDate)
			.Take(6)
			.ToList();

			List<ReviewDetail> AllReview = null;

			if (rating == 0)
			{
				// truy vấn lấy ra tất cả các đánh giá của 1 sản phẩm
				 AllReview = _reviewService.GetAllReview(ProductId);
			}



			// thông tin chi tiết của 1 sản phẩm
			var inforDetail = new ProductDetail()
			{
				Product = product,
				Images = imagesProduct,
				Colors = colorProduct,
				relatedProducts = relatedProduct,
			};

			ViewBag.AllReview = AllReview;

			return View(inforDetail);
		}

		[HttpGet]
		[Route("/Product/Reviews")]
		public IActionResult Review( int? rating)
		{
			// Đọc giá trị từ session
			int currentProductId = (int)HttpContext.Session.GetInt32("CurrentProductId");

			var Product = _context.TbProducts.FirstOrDefault(m => (m.ProductId == currentProductId) && (m.IsActive == true));
			if(Product == null)
			{
				return NotFound();
			}
			if(!rating.HasValue)
			{
				var AllReview = _reviewService.GetAllReview(currentProductId);
				return Ok(AllReview);
			}
			var ReviewByStar = _reviewService.GetReviewByStar(currentProductId , rating);

			return Ok(ReviewByStar);
		}

		[Route("/Product/Cart")]
		public IActionResult Cart()
		{
			return View();
		}

		
	}
}
