using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForumOnAnyTopic.Data;
using ForumOnAnyTopic.Models;
using Microsoft.AspNetCore.Authorization;

namespace ForumOnAnyTopic.Controllers
{
    public class PostController : Controller
    {
        private readonly ForumDB _context;
        public PostController(ForumDB context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Create(int? topicId)
        {
            UserNameInViewData();
            if (topicId == null)
                return NotFound();
            Post post = new Post()
            {
                TopicId = topicId,
                UserId = (await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name)).Id
            };
            return View(post);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Massage,TopicId,UserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreatDate = DateTime.Now;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return Redirect("/Topic/Details/" + post.TopicId);
            }
            return View(post);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            UserNameInViewData();
            if (id == null)
                return NotFound();

            var post = await _context.Posts
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
                return NotFound();
            if (post.User.Email != User.Identity.Name)
                return NotFound();
            return View(post);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Massage,CreatDate,TopicId,UserId")] Post post)
        {
            if (id != post.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                        return NotFound();
                    else
                        throw;
                }
                return Redirect("/Topic/Details/" + post.TopicId);
            }
            return View(post);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            UserNameInViewData();
            if (id == null)
                return NotFound();

            var post = await _context.Posts
                .Include(p => p.Topic)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
                return NotFound();
            if (post.User.Email != User.Identity.Name)
                return NotFound();

            return View(post);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return Redirect("/Topic/Details/" + post.TopicId);
        }

        [Authorize]
        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
        private void UserNameInViewData()
        {
            if (ViewData["UserName"] == null && User.Identity.Name != null)
                ViewData["UserName"] = (_context.Users.FirstOrDefault(u => u.Email == User.Identity.Name)).FirstName;
        }
    }
}
