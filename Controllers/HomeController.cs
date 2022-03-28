using ForumOnAnyTopic.Data;
using ForumOnAnyTopic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ForumOnAnyTopic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ForumDB _context;

        public HomeController(ForumDB context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            UserNameInViewData();
            var topics = await _context.Topics.Include(t => t.User).ToListAsync();
            return View(topics);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private void UserNameInViewData()
        {
            if (ViewData["UserName"] == null && User.Identity.Name != null)
                ViewData["UserName"] = (_context.Users.FirstOrDefault(u => u.Email == User.Identity.Name)).FirstName;
        }
    }
}
