using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Controllers
{
    public class BlogController : Controller
    {
        [Route("/Blog")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/Blog/Detail")]




        public IActionResult Detail()
        {
            return View();
        }
    }
}
