using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;

namespace SA_Online_Mart.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult ProcessOrder(Order order)
        {
            var cart = GetCart();
            // Save order and order details
            // Clear cart after order is processed
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("OrderConfirmation");
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }

        private Cart GetCart()
        {
            return HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart();
        }
    }

}
