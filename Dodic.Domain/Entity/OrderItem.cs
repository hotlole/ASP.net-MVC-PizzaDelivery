using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dodic.Domain.Entity
{
    public class OrderItem
    {
        public int Id { get; set; } // Идентификатор элемента заказа
        public int OrderId { get; set; } // Внешний ключ для связи с заказом
        public string Name { get; set; } 
        public int Quantity { get; set; } // Количество
        public decimal Price { get; set; } // Цена за единицу

        public Order Order { get; set; } // Навигационное свойство для заказа
    }
}
