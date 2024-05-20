using Castle.Core.Internal;
using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Models.ViewModels;
using DoAn_PTUDWEB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;

namespace DoAn_PTUDWEB.Controllers
{
	[Route("[controller]")]
    public class ProductController : Controller
	{
        private readonly ILogger<ProductController> _logger;
        private readonly DataContext _context;

		private readonly ReviewService _reviewService;
		private readonly ProductService _productService;
        public int PageSize = 9;


        public ProductController(ILogger<ProductController> logger, DataContext context, ReviewService reviewService, ProductService productService)
        {
            _logger = logger;
            _context = context;
			_reviewService = reviewService;
			_productService = productService;

		}
        // lọc sản phẩm theo các tiêu chí 
        [HttpPost]
        [Route("Filter")]
        public async Task<IActionResult> GetFilteredProducts([FromBody] FilterData filter)
        {
            
            var query = _context.TbProducts.AsQueryable();
            //lọc sản phẩm theo loaị mặt hàng
            if(filter.Categories.Count() > 0 )
            {
				query = query.Where(product => filter.Categories.Contains(product.CategoryProductId));
			}

            // lọc sản phẩm theo giá
            if (filter.PriceRange != null)
            {
                switch (filter.PriceRange)
                {
                    case "0-1000000":
                        query = query.Where(product => product.Price <= 1000000); break;
                    case "1000000-5000000":
                        query = query.Where(product => product.Price > 1000000 && product.Price < 5000000); break;
                    case "5000000-10000000":
                        query = query.Where(product => product.Price > 5000000 && product.Price < 10000000); break;
                    case "10000000-20000000":
                        query = query.Where(product => product.Price > 10000000 && product.Price < 20000000); break;
                    case "20000000-30000000":
                        query = query.Where(product => product.Price > 20000000 && product.Price < 30000000); break;
                    //  Các range khác tương tự
                    default:
                        break;
                }
            }
            // lọc sản phẩm theo màu
			if (filter?.Colors?.Count() > 0 && !filter.Colors[0].IsNullOrEmpty())
			{
				query = query.Where(p => p.TbProductColors.Any(pc => filter.Colors.Contains(pc.Color.ColorName)));
			}
            // lọc sản phẩm theo thương hiệu
            if (filter?.Trademarks?.Count() > 0 && !filter.Trademarks[0].IsNullOrEmpty())
            {
                query = query.Where(product => filter.Trademarks.Contains(product.Trademark.Name));
            }
            var products = await query.ToListAsync();
            return PartialView("_ReturnProducts", products);

            //return Ok(products);
        }

        // GET: Products by category
        [Route("LoaiMatHang/{CategoryId}")]
        public async Task<IActionResult> ProductsByCat(int CategoryId)
        {
            var productCategories = await _context.TbProducts.Where(product => product.CategoryProductId == CategoryId).ToListAsync();
            return View("ProductByCategories", productCategories);
        }

        // GET: lấy danh sách sản phẩm có phân trang
        public IActionResult Index(int ProductPage = 1)
        {
            return View(new ProductListViewModel
            {
                TbProducts = _context.TbProducts
                .Skip((ProductPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = ProductPage,
                    TotalItems = _context.TbProducts.Count(),
                }
            }
            );

        }

        //Tìm kiếm sản phẩm 
        [HttpPost]
        public IActionResult Search(string keywords, int ProductPage = 1)
        {
            return View("Index", new ProductListViewModel
            {
                TbProducts = _context.TbProducts
                .Where(p => p.Name.Contains(keywords.ToLower()))
                .Skip((ProductPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = ProductPage,
                    TotalItems = _context.TbProducts.Count(),
                }
            }
            );

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
			var colorProduct = _context.TbColors
				.Include(c => c.TbProductColors)
				.Where(c => c.TbProductColors.Any(pc => pc.ProductId == ProductId))
				.ToList();

			// truy vấn lấy các loại GB của sản phẩm
			var typeProduct = _context.TbTypes
                .Include(t => t.TbTypeProducts)
				.Where(t => t.TbTypeProducts.Any(tp => tp.ProductId == ProductId))
				.ToList();
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
                Types = typeProduct,
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

		

		
	}
}
