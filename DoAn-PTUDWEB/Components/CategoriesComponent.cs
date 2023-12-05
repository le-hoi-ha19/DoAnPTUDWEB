using DoAn_PTUDWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Components
{
	[ViewComponent(Name = "Categories")]

	public class CategoriesComponent : ViewComponent
    {
        private readonly DataContext _context;
        public CategoriesComponent(DataContext context)
        {
            _context = context;
        }
   
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofCategories = _context.TbProductCategories.ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofCategories));
        }
    }
}



