using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dodic.Service
{
	public class CartService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CartService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		private ISession Session => _httpContextAccessor.HttpContext.Session;

		public List<CartItem> GetCart()
		{
			var cartJson = Session.GetString("Cart");
			return string.IsNullOrEmpty(cartJson)
				? new List<CartItem>()
				: JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
		}

		public void AddToCart(CartItem item)
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
