using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface ICartRepository
    {
        Task<Cart> AddCartItemAsy( Cart Request);
        Task<List<Cart>> GetUserCartItems(string userId);
        Task<Cart?> GetCartItemAsync(string userId, int productId);
        Task<Cart> UpdateAsync(Cart cart);
        Task ClearCartAsync(string userId);
    }
}
