using EcomShopping.Data;
using EcomShopping.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcomShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }
        //get create 
        public ActionResult Create()
        {
            return View();
        }
        //post create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
        // get edit
        public ActionResult Edit(int? id)
        {
            var productTypes = _db.ProductTypes.Find(id);
            if (id == null)
            {
                return NotFound();
            }
            if (productTypes == null)
                return NotFound();
            return View(productTypes);
        }
        //post edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductTypes productTypes){
            if (ModelState.IsValid) 
            {
                _db.ProductTypes.Update(productTypes);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productTypes);
        }
        // get details
        public ActionResult Details(int? id)
        {
            var productTypes = _db.ProductTypes.Find(id);
            if (id == null)
                return NotFound();
            if (productTypes == null)
                return NotFound();
            return View(productTypes);
        }
        //post details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));
            
        }
        //get delete
        public ActionResult Delete(int? id)
        {
            var productTypes = _db.ProductTypes.Find(id);
            if (id == null)
                return NotFound();
            if (productTypes == null)
                return NotFound();
            return View(productTypes);
        }
        //post create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id,ProductTypes productTypes)
        {
            if (id == null)
                return NotFound();
            if (id != productTypes.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Remove(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
    }
}
