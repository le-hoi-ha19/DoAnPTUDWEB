using DoAn_PTUDWEB.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserProfileController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Password()
        {
            return View();
        }
    }
}
