using Imtahan.DataAccess;
using Imtahan.Models;
using Imtahan.ViewModel.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Imtahan.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class CategoryController(AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            var data = _context.Categories.ToList();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            Category c = new Category()
            {
                Name = vm.Name,
                Description = vm.Description,
            };
            await _context.Categories.AddAsync(c);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Categories.Where(x => x.Id == id).Select(x => new CategoryUpdateVM
            {
                Name = x.Name,
            }).FirstOrDefaultAsync();
            if (data is null) return NotFound();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM vm)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data is null) return NotFound();
            data.Name = vm.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Categories.FindAsync(id);
            if (data is null) return NotFound();
            _context.Categories.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Categories.FindAsync(id);
            if (data is null) return NotFound();
            data.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Categories.FindAsync(id);
            if (data is null) return NotFound();
            data.IsDeleted = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}




