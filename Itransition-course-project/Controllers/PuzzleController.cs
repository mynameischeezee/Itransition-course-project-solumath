using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Itransition_course_project.Data;
using Itransition_course_project.Models;

namespace Itransition_course_project.Controllers
{
    public class PuzzleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PuzzleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Puzzle
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts.Include(p => p.CreatedByUser).Include(p => p.Topic);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Puzzle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.CreatedByUser)
                .Include(p => p.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Puzzle/Create
        public IActionResult Create()
        {
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id");
            return View();
        }

        // POST: Puzzle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TopicId,CreatedByUserId,DateCreated,ImageUrl1,ImageUrl2,ImageUrl3,PostText,Answer1,Answer2,Answer3,Id")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id", post.CreatedByUserId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", post.TopicId);
            return View(post);
        }

        // GET: Puzzle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id", post.CreatedByUserId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", post.TopicId);
            return View(post);
        }

        // POST: Puzzle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,TopicId,CreatedByUserId,DateCreated,ImageUrl1,ImageUrl2,ImageUrl3,PostText,Answer1,Answer2,Answer3,Id")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

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
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id", post.CreatedByUserId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", post.TopicId);
            return View(post);
        }

        // GET: Puzzle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.CreatedByUser)
                .Include(p => p.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Puzzle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
