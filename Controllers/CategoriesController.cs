using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Models;
using System.Linq;

namespace Store.Controllers
{
    public class CategoriesController : Controller
    {
        /*private readonly AppDBContext db;
        public CategoriesController(AppDBContext context)
        {
            db = context;
        }*/
        private readonly ApplicationDbContext db;
        public CategoriesController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Index()
        {
            var categories = db.Categories;
            ViewBag.Categories = categories;
            return View();
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("New", category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                db.Categories.Update(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = db.Categories.Find(id);
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}


