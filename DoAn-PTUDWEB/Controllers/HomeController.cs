using DoAn_PTUDWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DoAn_PTUDWEB.Controllers
{
    public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
		public HomeController(ILogger<HomeController> logger, DataContext context)
		{
			_logger = logger;
            _context = context; 
		}

		public IActionResult Index()
        {
            return View();
        }

        [Route("/Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Contact")]
        public IActionResult Contact1(TbContact model)
        {
            if (ModelState.IsValid)
            {
                // Thực hiện lưu dữ liệu vào CSDL ở đây
                model.IsRead = false;
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = "System"; // Bạn có thể đặt giá trị này dựa trên người dùng đăng nhập

                _context.TbContacts.Add(model);
                _context.SaveChanges();

                // Gửi thông báo thành công đến view
                ViewBag.SuccessMessage = "Thông điệp của bạn đã được gửi thành công.";

                // Trả về view hiển thị thông báo
                return View("Contact");
            }

            // Nếu dữ liệu không hợp lệ, trả về view với model để hiển thị thông báo lỗi
            return View(model);
        }

		[Route("/About")]
		public IActionResult About()
		{
			return View();
		}

		




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}