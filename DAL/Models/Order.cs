using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum OrderStatus
    {
        Pending = 1,
        Cancelled = 2,
        Approved = 3,
        Shipped = 4,
        Delivered = 5
    }
    public enum PaymentMethod
    {
        Cash = 1,
        Visa = 2,
       
    }
    public class Order
    {
        public int Id { get; set; }

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public PaymentMethod PaymentMethod { get; set; }
        public string ?SeessionId { get; set; }
        public string ?PaymentIntentId { get; set; }
        public decimal? AmountPayed { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? UserId { get; set; }
        public ApplicationUsers ? User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
