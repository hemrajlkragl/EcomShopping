using EcomShopping.Data;
using EcomShopping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EcomShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IHostingEnvironment _he;
        public ProductController(ApplicationDbContext db,IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        //Get index method
        public IActionResult Index()
        {
            return View(_db.Products.Include(c=>c.ProductTypes).Include(c=>c.SpecialTag).ToList());
        }
        //post index method
        [HttpPost]
        public async Task<IActionResult> Index(decimal? lowAmount,decimal? highAmount)
        {

            var product = _db.Products.Include(c=>c.SpecialTag).Include(c=>c.ProductTypes).Where(c =>
            c.Price>=lowAmount && c.Price<=highAmount).ToList();
            if(lowAmount == null || highAmount == null)
            {
                product = _db.Products.Include(c => c.SpecialTag).Include(c => c.ProductTypes).ToList();
            }
            return View(product);
        }
        //get create
        public ActionResult Create(int? id)
        {
            
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTag.ToList(), "Id", "Name");
            return View();
        }
        //post create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products product, IFormFile image)
        {
            //if (ModelState.IsValid)
            //{
                var searchProduct = _db.Products.FirstOrDefault(c=>c.Name == product.Name);
                if (searchProduct != null)
                {
                    ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["TagId"] = new SelectList(_db.SpecialTag.ToList(), "Id", "Name");
                    ViewBag.message = "Product already exist!!";
                    return View(product);
                }
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/"+image.FileName;
                }
                if (image == null)
                    product.Image = "Images/noimage.PNG";
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(product);
        }
        //get edit
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTag.ToList(), "Id", "Name");
            if (id == null)
                return NotFound();
            var products = _db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).FirstOrDefault(c => c.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            
            return View(products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Products product, IFormFile image)
        {
            //if (ModelState.IsValid)
            //{
            if (image != null)
            {
                var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                await image.CopyToAsync(new FileStream(name, FileMode.Create));
                product.Image = "Images/" + image.FileName;
            }
            if (image == null)
                product.Image = "Images/noimage.PNG";
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //return View(product);
        }

        //get method for details
        public ActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var products = _db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).FirstOrDefault(c => c.Id == id);
            if (products == null)
                return NotFound();
            return View(products);
        }

        //get delete method
        public ActionResult Delete(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTag.ToList(), "Id", "Name");
            if (id == null) 
                return NotFound();
            var product = _db.Products.FirstOrDefault(c => c.Id == id);
            if(product == null) 
                return NotFound();
            return View(product);
        }
        //Post delete method

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ActionName("Delete")]
        public async Task<IActionResult> Delete(Products products)
        {
            _db.Products.Remove(products);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
