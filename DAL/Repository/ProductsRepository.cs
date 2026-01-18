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
    



}
}
