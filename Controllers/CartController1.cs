using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SA_Online_Mart.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICompositeViewEngine _viewEngine;

        public CartController(ApplicationDbContext context, ICompositeViewEngine viewEngine)
        {
            _context = context;
            _viewEngine = viewEngine;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                var cart = GetCart();
                cart.AddItem(product);
                SaveCart(cart);
            }

            // Prepare the updated cart HTML to return to the client
            var cartItemsHtml = RenderViewToString("_CartItemsPartial", GetCart());
            var cartTotal = GetCart().TotalValue.ToString("R 0.00");

            // Return the updated cart HTML and total as JSON
            return Json(new { cartItemsHtml, cartTotal });
        }

        // Add methods for RemoveFromCart, UpdateCart, etc.

        private Cart GetCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }

        // Helper method to render a view to a string
        private string RenderViewToString(string viewName, object model)
        {
            var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
            using (var sw = new StringWriter())
            {
                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    sw,
                    new HtmlHelperOptions()
                );

                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
