using EcomShopping.Data;
using EcomShopping.Models;
using EcomShopping.Utility;
using Microsoft.AspNetCore.Mvc;

namespace EcomShopping.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        //get checkout Action Method
        public IActionResult CheckOut()
        {
            return View();
        }
        //post checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order odr)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if(products != null)
            {
                foreach(var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = product.Id;
                    odr.OrderDetails.Add(orderDetails);
                }
            }
            odr.OrderNo = GetOrderNo();
            _db.Orders.Add(odr);
            await _db.SaveChangesAsync();
            HttpContext.Session.Set("products", null);
            return View();
        }
        public string GetOrderNo()
        {
            int rowCount = _db.Orders.ToList().Count() + 1;
            return rowCount.ToString("000");
        }
    }
}
