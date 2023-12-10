using DoAn_PTUDWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DoAn_PTUDWEB.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly DataContext _context;

        public BlogController(ILogger<BlogController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("/Blog")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/Blog/Detail/{id:long}.html", Name = "Details")]
        public IActionResult Details(long? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var post = _context.TbPosts
                .FirstOrDefault(m => (m.BlogId == id) && (m.IsActive == true));
            if (post == null)
            {
                return NotFound();
            }

            var comments = _context.TbPostComments
            .Where(c => c.BlogId == id)
            .Select(c => new TbPostComment
            {
                Name = c.Name,
                Detail = c.Detail,
            })
            .ToList();

            ViewBag.Post = post;
            ViewBag.Comments = comments;


            return View(post);


        }

        [HttpPost]
        [Route("/Blog/Detail/{id:int}.html", Name = "AddComment")]
        public IActionResult AddComment(int? id, TbPostComment comment)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _context.TbPosts
                .FirstOrDefault(m => m.BlogId == id && m.IsActive == true);

            if (post == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Thêm comment vào cơ sở dữ liệu
                comment.BlogId = id.Value;
                comment.CreatedDate = DateTime.Now;
                _context.TbPostComments.Add(comment);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id });
            }

            // Nếu dữ liệu không hợp lệ, quay lại view để hiển thị lỗi
            ViewBag.Post = post;
            ViewBag.Comments = _context.TbPostComments.Where(c => c.BlogId == id).ToList();
            return View("Details", post);
        }
    }
}