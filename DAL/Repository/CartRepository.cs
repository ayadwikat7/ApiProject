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
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDpContext _context;

        public CartRepository(ApplicationDpContext context)
        {
            _context = context;
        }
        public async Task<Cart> AddCartItemAsy(Cart Request)
        {
            await _context.Carts.AddAsync(Request);
            await _context.SaveChangesAsync();
            return Request;
        }

        public async Task<List<Cart>> GetUserCartItems(string userId)
        {
              return await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product).
                ThenInclude(c =>c.ProductTransulations)
                .ToListAsync();
        }
        public async Task<Cart?> GetCartItemAsync(string userId, int productId)
        {
            return await _context.Carts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        }
        public async Task<Cart> UpdateAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
        public async Task ClearCartAsync(string userId)
        {
            var items = await _context.Carts
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _context.Carts.RemoveRange(items);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteFromCartAsync(Cart cart) { 
        
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }


    }
}
