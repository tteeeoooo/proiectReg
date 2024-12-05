using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;

namespace Store.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext db;
        public ProductsController(ApplicationDbContext context)
        {
            db = context;
        }
        // metodele Index si Show sunt get, New si Edit sunt get si post, iar Delete e post
        [HttpGet]
        public IActionResult Index()
        {
            var l_products = db.Products.Include(p => p.Category).ToList();
            return View(l_products);
        }
        [HttpGet]
        public ActionResult Details(int id)// afisarea detaliilor unui singur produs in functie de id
        {
            var product = db.Products
                          .Include(p => p.Reviews) // Asigură încărcarea review-urilor asociate
                          .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpGet]
        public IActionResult Create()// fara param, doar pt render la un formular de creare al unui nou produs
        {
            var categories = db.Categories.ToList();
            ViewBag.AllCategories = categories;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)// suprascriere, e trimis un obiect de tip product
        {
            try
            {
                obj.DateListed = DateTime.Now;
                db.Products.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)// editarea unui obiect de tip product deja existent
        {
            Product? product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.EditedProduct = product;
            return View();
        }
        [HttpPost]
        public ActionResult Edit(int id, Product requested_obj)
        {
            Product? product = db.Products.Find(id);
            try
            {   // instantierea noului obiect de tip product pt adaugarea lui in bd
                product.Name = requested_obj.Name;
                product.Description = requested_obj.Description;
                product.DateListed = requested_obj.DateListed;
                product.Price = requested_obj.Price;
                product.Brand = requested_obj.Brand;
                product.Ingredients = requested_obj.Ingredients;
                db.SaveChanges();
                return RedirectToAction("Index");// dupa editare, user-ul e redirectionat catre view-ul index
            }
            catch (Exception)
            {
                return RedirectToAction("Edit", new { id = product.Id });
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)// delete nu are un view coresp
        {
            Product? product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);// metoda remove
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}