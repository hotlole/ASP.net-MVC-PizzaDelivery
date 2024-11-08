using Dodic.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }

    // Добавляем новые поля для данных клиента
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
