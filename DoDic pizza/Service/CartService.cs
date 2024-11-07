using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using DoDic_pizza.Models;
namespace DoDic_pizza.Service
{
	public class CartService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CartService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		private ISession Session => _httpContextAccessor.HttpContext.Session;

		public List<CartViewModel> GetCart()
		{
			var cartJson = Session.GetString("Cart");
			return string.IsNullOrEmpty(cartJson)
				? new List<CartViewModel>()
				: JsonConvert.DeserializeObject<List<CartViewModel>>(cartJson);
		}

		public void AddToCart(CartViewModel item)
		{
			var cart = GetCart();
			var existingItem = cart.FirstOrDefault(x => x.Name == item.Name);

			if (existingItem != null)
			{
				existingItem.Quantity += item.Quantity;
			}
			else
			{
				cart.Add(item);
			}

			Session.SetString("Cart", JsonConvert.SerializeObject(cart));
		}

		public void RemoveFromCart(string name)
		{
			var cart = GetCart();
			cart.RemoveAll(x => x.Name == name);
			Session.SetString("Cart", JsonConvert.SerializeObject(cart));
		}
	}
}
