using DoAn_PTUDWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Components
{
    [ViewComponent(Name = "Slide")]
    public class SlideComponent : ViewComponent
    {
        private readonly DataContext _context;
        public SlideComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofSlide = (from p in _context.TbImageSlides
                                 where (p.IsActive == true) 
                                 orderby p.SlideId ascending
                                 select p).Take(3).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofSlide));
        }
    }
}
