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
    public class SlideController : Controller
    {
        private readonly DataContext _context;

        public SlideController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/TbImageSlides
        public async Task<IActionResult> Index()
        {
              return _context.TbImageSlides != null ? 
                          View(await _context.TbImageSlides.ToListAsync()) :
                          Problem("Entity set 'DataContext.TbImageSlides'  is null.");
        }

        // GET: Admin/TbImageSlides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbImageSlides == null)
            {
                return NotFound();
            }

            var tbImageSlide = await _context.TbImageSlides
                .FirstOrDefaultAsync(m => m.SlideId == id);
            if (tbImageSlide == null)
            {
                return NotFound();
            }

            return View(tbImageSlide);
        }

        // GET: Admin/TbImageSlides/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TbImageSlides/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SlideId,path,IsActive")] TbImageSlide tbImageSlide)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbImageSlide);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbImageSlide);
        }

        // GET: Admin/TbImageSlides/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbImageSlides == null)
            {
                return NotFound();
            }

            var tbImageSlide = await _context.TbImageSlides.FindAsync(id);
            if (tbImageSlide == null)
            {
                return NotFound();
            }
            return View(tbImageSlide);
        }

        // POST: Admin/TbImageSlides/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SlideId,path,IsActive")] TbImageSlide tbImageSlide)
        {
            if (id != tbImageSlide.SlideId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbImageSlide);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbImageSlideExists(tbImageSlide.SlideId))
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
            return View(tbImageSlide);
        }

        // GET: Admin/TbImageSlides/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbImageSlides == null)
            {
                return NotFound();
            }

            var tbImageSlide = await _context.TbImageSlides
                .FirstOrDefaultAsync(m => m.SlideId == id);
            if (tbImageSlide == null)
            {
                return NotFound();
            }

            return View(tbImageSlide);
        }

        // POST: Admin/TbImageSlides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbImageSlides == null)
            {
                return Problem("Entity set 'DataContext.TbImageSlides'  is null.");
            }
            var tbImageSlide = await _context.TbImageSlides.FindAsync(id);
            if (tbImageSlide != null)
            {
                _context.TbImageSlides.Remove(tbImageSlide);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbImageSlideExists(int id)
        {
          return (_context.TbImageSlides?.Any(e => e.SlideId == id)).GetValueOrDefault();
        }
    }
}
