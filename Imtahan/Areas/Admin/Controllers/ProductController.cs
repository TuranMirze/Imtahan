using Imtahan.DataAccess;
using Imtahan.Models;
using Imtahan.ViewModel.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Reflection;
using System.Security.Claims;

namespace Imtahan.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class ProductController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public IActionResult Index()
        {
            var data = _context.Products.ToList();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            ViewBag.Products = await _context.Products.Where(x => x.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int? id, ProductCreateVM vm)
        {
            if (!id.HasValue) return BadRequest();
            if (!vm.CoverFile.ContentType.StartsWith(""))
            {
                ModelState.AddModelError("File", "Image deyil");
            }
            if (vm.CoverFile.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("File", "Olcusu uygun deyil");
            }

            string Filename = Path.GetRandomFileName() + Path.GetExtension(vm.CoverFile.FileName);

            using (Stream s = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs", "Employee", Filename)))
            {
                await vm.CoverFile.CopyToAsync(s);
            }

            Product p = new Product()
            {
                Name = vm.Name,
                Salary = vm.Salary,
                Color = vm.Color,
                CoverFile = Filename,
                CategoryId = vm.CategoryId,
            };
            await _context.Products.AddAsync(p);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Departments = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
            if (!id.HasValue) return BadRequest();
            var data = await _context.Products.Where(x => x.Id == id).Select(x => new ProductUpdateVM
            {
                Name = x.Name,
                Color = x.Color,
                Salary = x.Salary,
            }).FirstOrDefaultAsync();
            if (data is null) return NotFound();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, ProductUpdateVM vm)
        {

            if (!id.HasValue) return BadRequest();

            if (vm.CoverFile != null)
            {

                if (!vm.CoverFile.ContentType.StartsWith("image"))
                {
                    ModelState.AddModelError("File", "image deyil");
                }
                if (vm.CoverFile.Length > 1 * 1024 * 1024)
                {
                    ModelState.AddModelError("File", "1mb dan coxdu");
                }
            }
            ViewBag.Departments = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();


            var data = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data is null) return NotFound();
            if (vm.CoverFile != null)
            {

                string oldname = Path.Combine(_env.WebRootPath, "imgs", "Employee", data.CoverFile);


                using (Stream s = System.IO.File.Create(oldname))
                {
                    await vm.CoverFile!.CopyToAsync(s);
                }
            }
            data.Name = vm.Name;
            data.Salary = vm.Salary;
            data.Color = vm.Color;
            //data.CategoryId = vm.CategoryId
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Products.FindAsync(id);
            if (data is null) return NotFound();
            _context.Products.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Products.FindAsync(id);
            if (data is null) return NotFound();
            data.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Products.FindAsync(id);
            if (data is null) return NotFound();
            data.IsDeleted = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }

}


