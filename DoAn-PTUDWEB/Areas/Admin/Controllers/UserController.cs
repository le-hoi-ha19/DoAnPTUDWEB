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
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/User
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.TbUsers.Include(t => t.Role);
            return View(await dataContext.ToListAsync());
        }

        // GET: Admin/User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbUsers == null)
            {
                return NotFound();
            }

            var tbUser = await _context.TbUsers
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (tbUser == null)
            {
                return NotFound();
            }

            return View(tbUser);
        }

        // GET: Admin/User/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.TbRoles, "RoleId", "RoleName");
            return View();
        }

        // POST: Admin/User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,FullName,PasswordHash,Phone,Address,Email,Status,CreatedDate,ModifiedDate,RoleId")] TbUser tbUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.TbRoles, "RoleId", "RoleId", tbUser.RoleId);
            return View(tbUser);
        }

        // GET: Admin/User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbUsers == null)
            {
                return NotFound();
            }

            var tbUser = await _context.TbUsers.FindAsync(id);
            if (tbUser == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.TbRoles, "RoleId", "RoleName", tbUser.RoleId);
            return View(tbUser);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,FullName,PasswordHash,Phone,Address,Email,Status,CreatedDate,ModifiedDate,RoleId")] TbUser tbUser)
        {
            if (id != tbUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbUserExists(tbUser.UserId))
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
            ViewData["RoleId"] = new SelectList(_context.TbRoles, "RoleId", "RoleName", tbUser.RoleId);
            return View(tbUser);
        }

        // GET: Admin/User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbUsers == null)
            {
                return NotFound();
            }

            var tbUser = await _context.TbUsers
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (tbUser == null)
            {
                return NotFound();
            }

            return View(tbUser);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbUsers == null)
            {
                return Problem("Entity set 'DataContext.TbUsers'  is null.");
            }
            var tbUser = await _context.TbUsers.FindAsync(id);
            if (tbUser != null)
            {
                _context.TbUsers.Remove(tbUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbUserExists(int id)
        {
          return (_context.TbUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
