using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Data;
using SimpleShop.Models;
// Add this at the top with other using statements
using Microsoft.EntityFrameworkCore; 

namespace SimpleShop.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Welcome(string userName)
        {
        // This passes the raw string to the view
        ViewBag.Name = userName;
        return View();
        }
        // Add this method inside the HomeController class
    public IActionResult Search(string productName)
    {
    // CRITICAL VULNERABILITY: String concatenation in SQL allows a hacker 
    // to type "' OR 1=1 --" and see every single product or even delete the database.
        string query = "SELECT * FROM Products WHERE Name = '" + productName + "'";
    
        var products = _context.Products.FromSqlRaw(query).ToList();
        return View("Index", products);
    }
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }
    }
}