using Bulky.Web.Data;
using Bulky.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> categoryList = _context.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Category category)
        {

            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }

            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();
            
            return View(category);
        }
        
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            if (id == null && id == 0)
                return NotFound();
            
            Category category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            Category category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            
            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
