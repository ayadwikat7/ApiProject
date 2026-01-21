using DAL.Data;
using DAL.DTOs.Response;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ProductsRepository : IproductsRepository
    {
        private readonly ApplicationDpContext _context;

        public ProductsRepository(ApplicationDpContext context)
        {
            _context = context;
        }
        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<List<Product>> GetAllAsy()
        {
            return await _context.Products
                .Include(p => p.SubImages)              
                .Include(p => p.ProductTransulations)   
                .ToListAsync();
        }
        public async Task<Product?> FindByIdAsync(int id)//session 13 part1
        {
            return await _context.Products.Include(c => c.ProductTransulations).FirstOrDefaultAsync(c => c.Id == id);//session 13 part1
        }
        public IQueryable<Product> Query()
        {
            return _context.Products
                .Include(p => p.ProductTransulations)
                .AsQueryable();
        }
        public async Task<bool> DecreaseQuantitesAsync(List<(int productId, int quantity)> items)
        {
            var productIds = items.Select(p => p.productId).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

            foreach (var product in products)
            {
                var item = items.FirstOrDefault(p => p.productId == product.Id);

                if (product.Quantity < item.quantity)
                {
                    return false;
                }

                product.Quantity -= item.quantity;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
