using com.mobiquity.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace com.mobiquity.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult ParseFile()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ParseFile(DataFileViewModel dataFile)
        {
            //var items = _shoppingCart.GetShoppingCartItems();
            //_shoppingCart.ShoppingCartItems = items;

            //if (_shoppingCart.ShoppingCartItems.Count == 0)
            //{
            //    ModelState.AddModelError("", "Your cart is empty, add some pies first");
            //}

            //if (ModelState.IsValid)
            //{
            //    _orderRepository.CreateOrder(order);
            //    _shoppingCart.ClearCart();
            //    return RedirectToAction("CheckoutComplete");
            //}

            return View(dataFile);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}