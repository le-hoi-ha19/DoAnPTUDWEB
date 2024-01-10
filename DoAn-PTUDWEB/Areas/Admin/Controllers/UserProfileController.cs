using DoAn_PTUDWEB.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserProfileController : Controller
    {
        
        public IActionResult Index()
        {
            if (!Functions.IsLogin())
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public IActionResult Password()
        {
            return View();
        }
    }
}
