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
    }
}
