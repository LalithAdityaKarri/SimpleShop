using Microsoft.AspNetCore.Mvc;
using SimpleShop.Data;
using SimpleShop.Helpers;
using SimpleShop.Models;

namespace SimpleShop.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();
            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();
                cart.Add(product);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart");
            if (cart != null)
            {
                var itemToRemove = cart.FirstOrDefault(p => p.Id == id);
                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }
            return RedirectToAction("Index");
        }
    }
}