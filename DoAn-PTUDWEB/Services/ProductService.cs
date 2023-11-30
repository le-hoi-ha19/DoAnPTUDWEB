using DoAn_PTUDWEB.Models;
using Microsoft.CodeAnalysis;

namespace DoAn_PTUDWEB.Services
{
	public class ProductService
	{
		private readonly DataContext _context;

		public ProductService(DataContext context)
		{
			_context = context;
		}
		// Truy vấn sản phẩm theo id
		public TbProduct GetProductById(int productId)
		{
			var product = _context.TbProducts.FirstOrDefault(m => m.ProductId == productId && m.IsActive == true);

			return product;
		}

		// Truy vấn các ảnh khác của sản phẩm
		public List<TbImageProduct> GetImagesForProduct(int productId)
		{
			var imagesProduct = _context.TbImageProducts.Where(m => m.ProductId == productId).Take(4).ToList();

			return imagesProduct;
		}

		// Truy vấn các màu của 1 sản phẩm
		public List<TbColor> GetColorsForProduct(int productId)
		{
			var colorProduct = (from productColor in _context.TbProductColors
								join product in _context.TbProducts on productColor.ProductId equals product.ProductId
								join color in _context.TbColors on productColor.ColorId equals color.ColorId
								where product.ProductId == productId
								select new TbColor
								{
									ColorName = color.ColorName
								}).ToList();

			return colorProduct;
		}

		// Truy vấn sản phẩm liên quan và không hiển thị sản phẩm đang xem
		public List<TbProduct> GetRelatedProducts(int productId)
		{
			var relatedProducts = _context.TbProducts
				.Where(m => m.IsActive == true && m.ProductId != productId && _context.TbProducts
					.Where(p => p.ProductId == productId)
					.Select(p => p.CategoryProductId)
					.Contains(m.CategoryProductId))
				.OrderByDescending(m => m.CreatedDate)
				.Take(6)
				.ToList();

			return relatedProducts;
		}
	}
}
