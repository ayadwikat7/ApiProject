using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order request);
        Task<Order> GetBySessionIdAsync(string sessiomId);
        Task<Order> UpdateAsync(Order order);
        Task<bool> HasUserDeliverdOrderForProduct(string userId, int productId);
        Task<List<Order>> GetOrderByStatuesAsync(OrderStatus status);
        Task<Order?> GetOrderByIdAsync(int orderId);
    }
}
