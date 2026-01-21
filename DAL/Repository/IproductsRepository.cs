using DAL.DTOs.Response;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IproductsRepository
    {
        Task<List<Product>> GetAllAsy();
        Task<bool> DecreaseQuantitesAsync(List<(int productId, int quantity)> items);
        Task<Product> AddAsync(Product product);
        Task<Product?> FindByIdAsync(int id);
        IQueryable<Product> Query();
    }
}
