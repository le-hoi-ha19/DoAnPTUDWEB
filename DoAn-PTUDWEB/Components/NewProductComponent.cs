using DoAn_PTUDWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Components
{
	[ViewComponent(Name = "NewProduct")]
	public class NewProductComponent : ViewComponent
	{
		private readonly DataContext _context;
		public NewProductComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofProduct = (from p in _context.TbProducts
								 where (p.IsActive == true && p.IsHot == true && p.Quantity >=1)
								 orderby p.ProductId ascending
								 select p).Take(6).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofProduct));
		}
	}
}
