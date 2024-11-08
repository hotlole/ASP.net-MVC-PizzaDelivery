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
            var cart = _cartService.GetCart(); // получаем корзину

            // Проверяем, что корзина не пуста
            if (!cart.Any())
            {
                TempData["Error"] = "Ваша корзина пуста.";
                return RedirectToAction("Index", "Cart"); // Перенаправляем, если корзина пуста
            }

            // Если модель невалидна, возвращаем пользователя обратно на форму
            if (!ModelState.IsValid)
            {
                return View(orderViewModel);
            }

            // Создаем заказ
            var order = new Order
            {
                UserId = User.Identity.Name, // или User.FindFirstValue(ClaimTypes.NameIdentifier)
                OrderDate = DateTime.Now.ToUniversalTime(), // Преобразуем в UTC
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

            // Сохраняем заказ в базе данных
            _context.Orders.Add(order);
            _context.SaveChanges();

            // После создания заказа редиректим на страницу подтверждения с ID заказа
            return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
        }

        // Отображение страницы подтверждения заказа
        [HttpGet]
        public IActionResult OrderConfirmation(int orderId)
        {
            // Получаем заказ по ID с подгрузкой элементов заказа
            var order = _context.Orders
                .Include(o => o.OrderItems) // подгружаем элементы заказа
                .FirstOrDefault(o => o.Id == orderId);

            // Если заказ не найден, показываем ошибку
            if (order == null)
            {
                TempData["Error"] = "Заказ не найден.";
                return RedirectToAction("Index", "Home"); // Редирект на главную страницу
            }

            // Если заказ найден, возвращаем представление с заказом
            return View(order);
        }
    }
}
