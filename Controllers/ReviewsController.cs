using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Sephora.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext db;

        public ReviewsController(ApplicationDbContext context)
        {
            db = context;
        }

        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Index()
        {
            var reviews = db.Reviews.ToList();
            return View(reviews);
        }

        // Detaliile unei recenzii
        public IActionResult Details(int id)
        {
            var review = db.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // Crearea unei recenzii (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Crearea unei recenzii (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // Editarea unei recenzii (GET)
        public IActionResult Edit(int id)
        {
            var review = db.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // Editarea unei recenzii (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Update(review);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // Ștergerea unei recenzii (GET)
        public IActionResult Delete(int id)
        {
            var review = db.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // Ștergerea unei recenzii (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var review = db.Reviews.FirstOrDefault(r => r.Id == id);
            if (review != null)
            {
                db.Reviews.Remove(review);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}