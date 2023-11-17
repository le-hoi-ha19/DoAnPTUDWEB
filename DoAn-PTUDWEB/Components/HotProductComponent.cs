using DoAn_PTUDWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Components
{
	[ViewComponent(Name = "HotProduct")]
	public class HotProductComponent : ViewComponent
	{
		private readonly DataContext _context;
		public HotProductComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofProduct = (from p in _context.Products
								 where (p.IsActive == true) && (p.IsHot == true)
								 orderby p.ProductId descending
								 select p).Take(6).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofProduct));
		}
	}
}
