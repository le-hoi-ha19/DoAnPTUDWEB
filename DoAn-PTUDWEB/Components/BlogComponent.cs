using DoAn_PTUDWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Components
{
    [ViewComponent(Name = "Blog")]
    public class BlogComponent : ViewComponent
    {
        private readonly DataContext _context;

        public BlogComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofBlog = (from p in _context.TbPosts
                              where p.IsActive == true
                              select p).Take(10).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofBlog));
        }
    }
}