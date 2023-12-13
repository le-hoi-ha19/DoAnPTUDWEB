using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAn_PTUDWEB.Models;
using X.PagedList;

namespace DoAn_PTUDWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrademarkController : Controller
    {
        private readonly DataContext _context;

        public TrademarkController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Trademark
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1; // Nếu page là null, mặc định hiển thị trang đầu tiên
            var pageSize = 6; // Số lượng mục trên mỗi trang

            var trademarks = await _context.TbTrademarks
                .OrderBy(t => t.TrademarkId) 
                .ToPagedListAsync(pageNumber, pageSize);

            return View(trademarks);
        }

        // GET: Admin/Trademark/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbTrademarks == null)
            {
                return NotFound();
            }

            var tbTrademark = await _context.TbTrademarks
                .FirstOrDefaultAsync(m => m.TrademarkId == id);
            if (tbTrademark == null)
            {
                return NotFound();
            }

            return View(tbTrademark);
        }

        // GET: Admin/Trademark/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Trademark/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrademarkId,Name,Description,Logo")] TbTrademark tbTrademark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbTrademark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbTrademark);
        }

        // GET: Admin/Trademark/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbTrademarks == null)
            {
                return NotFound();
            }

            var tbTrademark = await _context.TbTrademarks.FindAsync(id);
            if (tbTrademark == null)
            {
                return NotFound();
            }
            return View(tbTrademark);
        }

        // POST: Admin/Trademark/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrademarkId,Name,Description,Logo")] TbTrademark tbTrademark)
        {
            if (id != tbTrademark.TrademarkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbTrademark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbTrademarkExists(tbTrademark.TrademarkId))
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
            return View(tbTrademark);
        }

        // GET: Admin/Trademark/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbTrademarks == null)
            {
                return NotFound();
            }

            var tbTrademark = await _context.TbTrademarks
                .FirstOrDefaultAsync(m => m.TrademarkId == id);
            if (tbTrademark == null)
            {
                return NotFound();
            }

            return View(tbTrademark);
        }

        // POST: Admin/Trademark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbTrademarks == null)
            {
                return Problem("Entity set 'DataContext.TbTrademarks'  is null.");
            }
            var tbTrademark = await _context.TbTrademarks.FindAsync(id);
            if (tbTrademark != null)
            {
                _context.TbTrademarks.Remove(tbTrademark);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbTrademarkExists(int id)
        {
          return (_context.TbTrademarks?.Any(e => e.TrademarkId == id)).GetValueOrDefault();
        }
    }
}
