using DoAn_PTUDWEB.Constains;
using DoAn_PTUDWEB.Filters;
using DoAn_PTUDWEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize]
	[AdminRequired]
	public class ContactController : Controller
    {
        private readonly DataContext _context;
        public ContactController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var contactlist = _context.TbContacts.OrderBy(m => m.ContactId).ToList();
            return View(contactlist);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.TbContacts.Find(id);

            if(mn == null)
            {
                return NotFound();
            }

            return View(mn);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleContact = _context.TbContacts.Find(id);
            if(deleContact == null)
            {
                return NotFound();
            }
            _context.TbContacts.Remove(deleContact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
