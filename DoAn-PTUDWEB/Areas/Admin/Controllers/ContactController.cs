using DoAn_PTUDWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_PTUDWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
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
    }
}
