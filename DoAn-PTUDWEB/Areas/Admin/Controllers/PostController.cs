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
    public class PostController : Controller
    {
        private readonly DataContext _context;

        public PostController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Post
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.TbPosts.Include(t => t.User);
            return View(await dataContext.ToListAsync());
        }

        // GET: Admin/Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbPosts == null)
            {
                return NotFound();
            }

            var tbPost = await _context.TbPosts
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (tbPost == null)
            {
                return NotFound();
            }

            return View(tbPost);
        }

        // GET: Admin/Post/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.TbUsers, "UserId", "UserId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,Name,Description,Detail,Image,SeoTitle,SeoDescription,SeoKeywords,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,UserId,IsActive")] TbPost tbPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.TbUsers, "UserId", "UserId", tbPost.UserId);
            return View(tbPost);
        }

        // GET: Admin/Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbPosts == null)
            {
                return NotFound();
            }

            var tbPost = await _context.TbPosts.FindAsync(id);
            if (tbPost == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.TbUsers, "UserId", "UserId", tbPost.UserId);
            return View(tbPost);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,Name,Description,Detail,Image,SeoTitle,SeoDescription,SeoKeywords,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,UserId,IsActive")] TbPost tbPost)
        {
            if (id != tbPost.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbPostExists(tbPost.BlogId))
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
            ViewData["UserId"] = new SelectList(_context.TbUsers, "UserId", "UserId", tbPost.UserId);
            return View(tbPost);
        }

        // GET: Admin/Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbPosts == null)
            {
                return NotFound();
            }

            var tbPost = await _context.TbPosts
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (tbPost == null)
            {
                return NotFound();
            }

            return View(tbPost);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbPosts == null)
            {
                return Problem("Entity set 'DataContext.TbPosts'  is null.");
            }
            var tbPost = await _context.TbPosts.FindAsync(id);
            if (tbPost != null)
            {
                _context.TbPosts.Remove(tbPost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbPostExists(int id)
        {
          return (_context.TbPosts?.Any(e => e.BlogId == id)).GetValueOrDefault();
        }
    }
}
