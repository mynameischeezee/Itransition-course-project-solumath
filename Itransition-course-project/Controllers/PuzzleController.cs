using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Itransition_course_project.Data;
using Itransition_course_project.Models;
using Itransition_course_project.Services.CloudServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Itransition_course_project.Controllers
{
    [Authorize]
    public class PuzzleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICloudStorage _cloudStorage;

        public PuzzleController(ApplicationDbContext context, ICloudStorage cloudStorage)
        {
            _context = context;
            _cloudStorage = cloudStorage;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts.Include(p => p.CreatedByUser).Include(p => p.Topic);
            return View(await applicationDbContext.ToListAsync());
        }
        
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
        private static string FormFileName(string title, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{title}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
            return fileNameForStorage;
        }
        private async Task UploadFiles(Post post, IFormFile file1, IFormFile file2, IFormFile file3)
        {
            if (file1 != null)
            {
                string fileNameForStorage = FormFileName(post.Name, file1.FileName);
                post.ImageUrl1 = await _cloudStorage.UploadFileAsync(post.ImageFile1, fileNameForStorage);
                post.ImageStorageName1 = fileNameForStorage;
            }
            if (file2 != null)
            {
                string fileNameForStorage = FormFileName(post.Name, file2.FileName);
                post.ImageUrl2 = await _cloudStorage.UploadFileAsync(post.ImageFile2, fileNameForStorage);
                post.ImageStorageName2 = fileNameForStorage;
            }
            if (file3 != null)
            {
                string fileNameForStorage = FormFileName(post.Name, file3.FileName);
                post.ImageUrl3 = await _cloudStorage.UploadFileAsync(post.ImageFile3, fileNameForStorage);
                post.ImageStorageName3 = fileNameForStorage;
            }
        }

        public IActionResult Create()
        {
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TopicId,CreatedByUserId,DateCreated,ImageUrl1,ImageUrl2,ImageUrl3,ImageFile1,ImageFile2,ImageFile3,PostText,Answer1,Answer2,Answer3,Id")] Post post)
        {
            if (ModelState.IsValid)
            {
                await UploadFiles(post, post.ImageFile1, post.ImageFile2, post.ImageFile3);
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id", post.CreatedByUserId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", post.TopicId);
            return View(post);
        }
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
