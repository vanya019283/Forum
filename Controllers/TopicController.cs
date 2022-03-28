using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForumOnAnyTopic.Data;
using ForumOnAnyTopic.Models;
using Microsoft.AspNetCore.Authorization;

namespace ForumOnAnyTopic.Controllers
{
    public class TopicController : Controller
    {
        private readonly ForumDB _context;

        public TopicController(ForumDB context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int? id)
        {
            UserNameInViewData();

            if (id == null)
                return NotFound();

            Topic topic = await _context.Topics
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
                return NotFound();

            var posts = await _context.Posts.Include(p => p.Topic).Include(p => p.User).ToListAsync();
            return View(new KeyValuePair<Topic, List<Post>>(topic, posts));
        }

        [Authorize]
        public IActionResult Create()
        {
            UserNameInViewData();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.UserId = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name).Id;
                topic.CreatedDate = DateTime.Now;
                _context.Add(topic);
                await _context.SaveChangesAsync();
                return Redirect("/Home/Index");
            }
            return View(topic);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            UserNameInViewData();
            if (id == null)
                return NotFound();
            var topic = await _context.Topics
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
                return NotFound();
            if (topic.User.Email != User.Identity.Name)
                return NotFound();
            return View(topic);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreatedDate,UserId")] Topic topic)
        {
            if (id != topic.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(topic.Id))
                        return NotFound();
                    else
                        throw;
                }
                return Redirect("/Home/Index");
            }
            return View(topic);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            UserNameInViewData();
            if (id == null)
                return NotFound();

            var topic = await _context.Topics
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
                return NotFound();
            if (topic.User.Email != User.Identity.Name)
                return NotFound();
            return View(topic);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
            return Redirect("/Home/Index");
        }

        private bool TopicExists(int id)
        {
            return _context.Topics.Any(e => e.Id == id);
        }
        private async void UserNameInViewData()
        {
            if (ViewData["UserName"] == null && User.Identity.Name != null)
                ViewData["UserName"] = (await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name)).FirstName;
        }
    }
}
