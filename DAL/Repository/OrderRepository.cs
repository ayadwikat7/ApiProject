using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDpContext _context;

        public OrderRepository(ApplicationDpContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order request)
        {
            await _context.Orders.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<Order> GetBySessionIdAsync(string sessionId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.SeessionId == sessionId);
        }


        public async Task<bool> HasUserDeliverdOrderForProduct(string userId, int productId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId && o.OrderStatus == OrderStatus.Delivered)
                .SelectMany(o => o.OrderItems)
                .AnyAsync(oi => oi.ProductId == productId);
        }

        public async Task<List<Order>> GetOrderByStatuesAsync(OrderStatus status)
        {
            return await _context.Orders
                    .Where(o => o.OrderStatus == status).Include(o => o.User)
                    .ToListAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.
            Include(o => o.User)
            .Include(o => o.OrderItems)
            .ThenInclude(o => o.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order> UpdateAsync(Order order) {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;

        }
} }

