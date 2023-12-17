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
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(int? page)
        {
            const int pageSize = 4; // Số lượng sản phẩm trên mỗi trang

            var dataContext = _context.TbProducts.Include(t => t.CategoryProduct).Include(t => t.Trademark);

            var pageNumber = page ?? 1; // Nếu page là null, sử dụng trang đầu tiên

            var pagedProducts = await dataContext.ToPagedListAsync(pageNumber, pageSize);

            return View(pagedProducts);
        }


        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProducts == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.TbProducts
                .Include(t => t.CategoryProduct)
                .Include(t => t.Trademark)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryName"] = new SelectList(_context.TbProductCategories, "Name", "Name");
            ViewData["TrademarkName"] = new SelectList(_context.TbTrademarks, "Name", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CategoryProductId,Name,IsBestSeller,IsHot,IsNew,Price,PriceDiscount,Quantity,Thumbnail,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,TrademarkId,Deleted,Description,IsActive")] TbProduct tbProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryProductId"] = new SelectList(_context.TbProductCategories, "CategoryProductId", "CategoryProductId", tbProduct.CategoryProductId);
            ViewData["TrademarkId"] = new SelectList(_context.TbTrademarks, "TrademarkId", "TrademarkId", tbProduct.TrademarkId);
            return View(tbProduct);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbProducts == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.TbProducts.FindAsync(id);
            if (tbProduct == null)
            {
                return NotFound();
            }
            ViewData["CategoryProductId"] = new SelectList(_context.TbProductCategories, "CategoryProductId", "CategoryProductId", tbProduct.CategoryProductId);
            ViewData["TrademarkId"] = new SelectList(_context.TbTrademarks, "TrademarkId", "TrademarkId", tbProduct.TrademarkId);
            return View(tbProduct);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryProductId,Name,IsBestSeller,IsHot,IsNew,Price,PriceDiscount,Quantity,Thumbnail,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,TrademarkId,Deleted,Description,IsActive")] TbProduct tbProduct)
        {
            if (id != tbProduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProductExists(tbProduct.ProductId))
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
            ViewData["CategoryProductId"] = new SelectList(_context.TbProductCategories, "CategoryProductId", "CategoryProductId", tbProduct.CategoryProductId);
            ViewData["TrademarkId"] = new SelectList(_context.TbTrademarks, "TrademarkId", "TrademarkId", tbProduct.TrademarkId);
            return View(tbProduct);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbProducts == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.TbProducts
                .Include(t => t.CategoryProduct)
                .Include(t => t.Trademark)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbProducts == null)
            {
                return Problem("Không tìm thấy sản phẩm");
            }
            var Colors= _context.TbProductColors.Where(pc => pc.ProductId == id);
            _context.TbProductColors.RemoveRange(Colors);
            _context.SaveChanges();

            var Images = _context.TbImageProducts.Where(pc => pc.ProductId == id);
            _context.TbImageProducts.RemoveRange(Images);
            _context.SaveChanges();

            var tbProduct = await _context.TbProducts.FindAsync(id);
            if (tbProduct != null)
            {
                _context.TbProducts.Remove(tbProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        private bool TbProductExists(int id)
        {
          return (_context.TbProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
