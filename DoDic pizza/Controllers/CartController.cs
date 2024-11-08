using DoDic_pizza.Models;
using DoDic_pizza.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DoDic_pizza.Controllers
{
	
		public class CartController : Controller
		{
			private readonly CartService _cartService;

			public CartController(CartService cartService)
			{
				_cartService = cartService;
			}

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();

            // Вычисление суммы товаров в корзине
            var totalPrice = cart.Sum(x => decimal.Parse(x.Price) * x.Quantity);

            // Передаем сумму в ViewData для отображения в Layout
            ViewData["CartTotal"] = totalPrice.ToString("F2");

            return View(cart);
        }


        [HttpPost]
			public IActionResult AddToCart(string name, string price)
			{
				_cartService.AddToCart(new CartViewModel { Name = name, Price = price, Quantity = 1 });
				return RedirectToAction("Index", "Cart");
			}

			[HttpPost]
			public IActionResult RemoveFromCart(string name)
			{
				_cartService.RemoveFromCart(name);
				return RedirectToAction("Index", "Cart");
			}
		}


	
}
