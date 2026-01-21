using DAL.DTOs.Request;
using DAL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IProductService
    {
        Task<List<ProductsResponse>> GetAllproductsForAdmin();
        Task<ProductsResponse> CreateProduct(ProductsRequest request);
        Task<PaginatedResponse<ProductsUserResponse>> GetAllProductsForUser(
             string lan = "en", int page = 1,
             int limit = 3, string? search = null,
                 int? categoryId = null,
     decimal? minPrice = null,
     decimal? maxPrice = null);

        Task<ProductsUserDetailes> GetAllProductsDetailsForUser(int id, string lan = "en");
    }
}
