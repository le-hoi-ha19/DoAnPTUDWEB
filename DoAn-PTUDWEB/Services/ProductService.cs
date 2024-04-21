using DoAn_PTUDWEB.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

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

		// Truy vấn sản phẩm liên quan và không hiển thị sản phẩm đang xem
		public List<TbProduct> GetRelatedProducts(int productId)
		{
			var relatedProducts = _context.TbProducts
				.Where(m => m.IsActive == true && m.ProductId != productId && _context.TbProducts
					.Where(p => p.ProductId == productId)
					.Select(p => p.CategoryProductId)
					.Contains(m.CategoryProductId))
				.OrderByDescending(m => m.ProductId)
				.Take(6)
				.ToList();

			return relatedProducts;
		}
	}
}
