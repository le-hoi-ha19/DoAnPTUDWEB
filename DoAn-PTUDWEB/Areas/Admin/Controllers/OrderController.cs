using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAn_PTUDWEB.Models;
using DoAn_PTUDWEB.Models.ViewModels;
using DoAn_PTUDWEB.Constains;
using Microsoft.AspNetCore.Authorization;
using DoAn_PTUDWEB.Filters;

namespace DoAn_PTUDWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
	[Authorize]
	[AdminRequired]
	public class OrderController : Controller
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Order
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
			var query = _context.TbOrders.AsQueryable();
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
			}).ToList();

			ViewBag.orders = orders;
			return View();
        }

        // GET: Admin/Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbOrders == null)
            {
                return NotFound();
            }

			
			var query = _context.TbOrders.AsQueryable();
            query = query.Where(o => o.OrderId == id );
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
            //return Ok(orders);
        }



      
    

        // GET: Admin/Order/Edit/5
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbOrders == null)
            {
                return NotFound();
            }

            var tbOrder = await _context.TbOrders.FindAsync(id);
            if (tbOrder == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.TbUsers, "UserId", "FullName", tbOrder.UserId);
            return View(tbOrder);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("UserId,OrderId,OrderDate,ShipAddress,Note,Status")] TbOrder tbOrder)
        {
            if (tbOrder == null)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(tbOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbOrderExists(tbOrder.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
          
        }

        public class OrderStatus
        {
            public int status { get; set; }
            public int OrderId { get; set; }

        }

		[HttpPatch]
		[Route("Change/Status")]
		public async Task<IActionResult> Change([FromBody] OrderStatus obj)
		{
			if (obj == null)
			{
				return BadRequest(); // Thay vì NotFound()
			}

			var newOrder = await _context.TbOrders.FindAsync(obj.OrderId);
			if (newOrder == null)
			{
				return NotFound();
			}

			newOrder.Status = obj.status;

			if (obj.status == 4)
			{
				var orderDetailsList = _context.TbOrderDetails.Where(o => o.OrderId == obj.OrderId).ToList();
				foreach (var orderDetail in orderDetailsList)
				{
					var product = _context.TbProducts.FirstOrDefault(p => p.ProductId == orderDetail.ProductId);
					if (product != null)
					{
						product.Quantity += orderDetail.Quantity;
					}
				}
			}


			_context.SaveChanges();

			return Ok();
		}


		// POST: Admin/Order/Delete/5
		[HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {

            var tbOrder = _context.TbOrders
                     .Where(b => b.OrderId == id)
                     .Include("TbOrderDetails")
                     .FirstOrDefault();

            if (tbOrder != null)
            {
                _context.Remove(tbOrder);
                 _context.SaveChanges();

            }
            
            return Ok();
        }

        private bool TbOrderExists(int id)
        {
          return (_context.TbOrders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
