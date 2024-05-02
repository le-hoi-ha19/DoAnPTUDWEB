using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Models.ViewModels;
using DoAn_PTUDWEB.Services;
using DoAn_PTUDWEB.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Text.Json;

namespace DoAn_PTUDWEB.Controllers
{
	public class AccountController : Controller
	{
        private readonly DataContext _context;
        public AccountController( DataContext context)
        {
            _context = context;
        }
        [Route("/Account")]
		public IActionResult Index()
		{
			int id = 2; // id người dùng khi đăng nhập
            var user = _context.TbUsers.FirstOrDefault(u =>u.UserId == id);
            var quantityOrderConfirm = _context.TbOrders.Where(o => o.Status == 1 && o.UserId == id).Count();
            var quantityOrderDeliver = _context.TbOrders.Where(o => o.Status == 2 && o.UserId == id).Count();
            var quantityOrderComplete = _context.TbOrders.Where(o => o.Status == 3 && o.UserId == id).Count();
            var quantityOrderCancel = _context.TbOrders.Where(o => o.Status == 4 && o.UserId == id).Count();

          


            if (user != null)
            {
                ViewBag.User = user;
                ViewBag.orderConfirm = quantityOrderConfirm;
                ViewBag.orderDeliver = quantityOrderDeliver;
                ViewBag.orderComplete = quantityOrderDeliver;
                ViewBag.orderCancel = quantityOrderCancel;

			}

            return View();
			
		}

        [HttpGet]
		[Route("/Account/Order")]
		public IActionResult Order(int status)
        {
            int id = 2;
            // id người dùng khi đăng nhập
            var query = _context.TbOrders.AsQueryable();
            query = query.Where(o => o.UserId == id && o.Status == status);

            var orders = query.Select(order => new OrderViewModel
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                CustomerName = order.User.FullName,
                Phone = order.User.Phone,
                Email = order.User.Email,
                Note = order.Note,
                ShipAddress = order.ShipAddress,
                Status = order.Status,
                Details = order.TbOrderDetails.Select(od => new OrderDetailViewModel
                {
                    ProductId = od.ProductId,
                    Name = od.Product.Name,
                    ProductImage = od.Product.TbImageProducts.FirstOrDefault().Thumbnail ?? null,
                    Price = od.Price,
                    ProductColorId = od.ColorId,
                    ProductColor = od.Color != null ? od.Color.ColorName : null,
                    ProductTypeId = od.TypeId,
                    ProductType = od.Type != null ? od.Type.TypeName : null,
                    Quantity = od.Quantity,
                    TotalMoney = od.Price * od.Quantity,
                }).ToList(),
            }).ToList();
   
            ViewBag.orders = orders;
            return View();
        }
		public class AccountViewModel
        {
            public int IdUser { get; set; }
            public string? FullName { get; set; }
            public string? PasswordHash { get; set; }
            public string? Email { get; set; }
            public string? Address { get; set; }
            public string? Phone { get; set; }
        }

        [HttpPatch("Edit")]
        [Route("/Account/Edit")]
        public IActionResult Edit([FromBody] AccountViewModel user)
        {
            if (user == null)
            {
                return NotFound();
            }
           
            var newUser = _context.TbUsers.FirstOrDefault(x=>x.UserId == user.IdUser);
            if (newUser == null)
            {
                return NotFound();

            }
            newUser.FullName = user.FullName;
            newUser.PasswordHash = user.PasswordHash;
            newUser.Email = user.Email;
            newUser.Phone = user.Phone;
            newUser.Address = user.Address;
            _context.SaveChanges();

            return Ok();
        }

        [Route("/Account/Setting")]
        public IActionResult Setting()
        {
			int id = 4; // id người dùng khi đăng nhập
			var user = _context.TbUsers.FirstOrDefault(u => u.UserId == id);

			return View(user);
        }
		[Route("/Account/Logout")]
		public IActionResult Logout()
		{

			return View();
		}
	}
}
