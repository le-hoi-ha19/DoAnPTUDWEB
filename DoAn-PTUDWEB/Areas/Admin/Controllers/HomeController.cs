using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DoAn_PTUDWEB.Filters;
using System.Security.Claims;

namespace DoAn_PTUDWEB.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	[AdminRequired]
	public class HomeController : Controller
    {
		private readonly DataContext _context;

		public HomeController(DataContext context)
		{
			_context = context;
		}

		
		public IActionResult Index()
        {
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
			if (userIdClaim != null)
			{
				string userId = userIdClaim.Value;
				var user = _context.TbUsers.FirstOrDefault(u => u.UserId.ToString() == userId);
				if (user != null)
				{
					HttpContext.Session.SetString("FullName", user.FullName);
				}
			}

			var listOrders = _context.TbOrders.ToList().Where(x=>x.Status ==1).Take(6);
			var listBrand = _context.TbTrademarks.ToList().Take(6);
			var orderViewModels = new List<OrderViewModel>();

			foreach (var order in listOrders)
			{
				var inforOrder = new OrderViewModel
				{
					OrderId = order.OrderId,
					OrderDate = order.OrderDate,
					CustomerName = _context.TbUsers.FirstOrDefault(u => u.UserId == order.UserId)?.FullName,
					Status = order.Status,
				};

				orderViewModels.Add(inforOrder);
			}

			var quantityOrder = listOrders.Count(x => x.Status == 1);
			var quantityUser = _context.TbUsers.Count();
			var sale = _context.TbOrderDetails.Sum(o => o.TotalMoney ?? 0);

			ViewBag.quantityOrder = quantityOrder;
			ViewBag.quantityUser = quantityUser;
			ViewBag.sale = sale;
			ViewBag.orderViewModels = orderViewModels;
			ViewBag.listBrand = listBrand;
			return View();

		}
	}
}
