using ForumOnAnyTopic.Data;
using ForumOnAnyTopic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
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
            var topics = _context.Topics.Include(t => t.User);
            return View(await topics.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async void UserNameInViewData()
        {
            if (ViewData["UserName"] == null && User.Identity.Name != null)
                ViewData["UserName"] = (await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name)).FirstName;
        }
    }
}
