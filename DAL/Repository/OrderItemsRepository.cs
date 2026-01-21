using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class OrderItemsRepository: IOderItemsRepository
    {
        private readonly ApplicationDpContext _context;

        public OrderItemsRepository(ApplicationDpContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<OrderItem> orderItems)
        {
            await _context.AddRangeAsync(orderItems);
            await _context.SaveChangesAsync();
          
        }
    }
}
