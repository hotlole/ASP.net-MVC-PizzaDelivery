using Dodic.DAL;
using Dodic.Domain.Entity;
using DoDic_pizza.Models;
using DoDic_pizza.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DoDic_pizza.Controllers
{
    public class OrderController : Controller
    {
        private readonly CartService _cartService;
        private readonly ApplicationContext _context;

        public OrderController(CartService cartService, ApplicationContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        // Отображение формы для создания заказа
        [HttpGet]
        public IActionResult CreateOrder()
        {
            var model = new OrderViewModel(); // создаем модель
            return View(model); // возвращаем форму
        }

        // Обработка POST запроса для создания заказа
        [HttpPost]
        public IActionResult CreateOrder(OrderViewModel orderViewModel)
        {
            var cart = _cartService.GetCart();

            if (!cart.Any())
            {
                TempData["Error"] = "Ваша корзина пуста.";
                return RedirectToAction("Index", "Cart");
            }

            if (!ModelState.IsValid)
            {
                return View(orderViewModel);
            }

            var order = new Order
            {
                UserId = User.Identity.Name,
                OrderDate = DateTime.UtcNow,
                TotalAmount = cart.Sum(c => decimal.Parse(c.Price) * c.Quantity),
                Status = "В обработке",
                Name = orderViewModel.Name,
                Address = orderViewModel.Address,
                Phone = orderViewModel.Phone,
                OrderItems = cart.Select(item => new OrderItem
                {
                    Name = item.Name,
                    Price = decimal.Parse(item.Price),
                    Quantity = item.Quantity
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
        }


        // Отображение страницы подтверждения заказа
        [HttpGet]
        public IActionResult OrderConfirmation(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                TempData["Error"] = "Заказ не найден.";
                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }

    }
}
