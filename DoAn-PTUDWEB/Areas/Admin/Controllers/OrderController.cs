using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAn_PTUDWEB.Models;

namespace DoAn_PTUDWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Order
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.TbOrders.Include(t => t.User);
            return View(await dataContext.ToListAsync());
        }

        // GET: Admin/Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbOrders == null)
            {
                return NotFound();
            }

            var tbOrder = await _context.TbOrders
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (tbOrder == null)
            {
                return NotFound();
            }

            return View(tbOrder);
        }

        // GET: Admin/Order/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.TbUsers, "UserId", "UserId");
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,OrderId,OrderDate,ShipAddress,Note,Status")] TbOrder tbOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.TbUsers, "UserId", "UserId", tbOrder.UserId);
            return View(tbOrder);
        }

        // GET: Admin/Order/Edit/5
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
            ViewData["UserId"] = new SelectList(_context.TbUsers, "UserId", "UserId", tbOrder.UserId);
            return View(tbOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,OrderId,OrderDate,ShipAddress,Note,Status")] TbOrder tbOrder)
        {
            if (id != tbOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            ViewData["UserId"] = new SelectList(_context.TbUsers, "UserId", "UserId", tbOrder.UserId);
            return View(tbOrder);
        }

        // GET: Admin/Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbOrders == null)
            {
                return NotFound();
            }

            var tbOrder = await _context.TbOrders
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (tbOrder == null)
            {
                return NotFound();
            }

            return View(tbOrder);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbOrders == null)
            {
                return Problem("Entity set 'DataContext.TbOrders'  is null.");
            }
            var tbOrder = await _context.TbOrders.FindAsync(id);
            if (tbOrder != null)
            {
                _context.TbOrders.Remove(tbOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbOrderExists(int id)
        {
          return (_context.TbOrders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
