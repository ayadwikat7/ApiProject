using DAL.DTOs.Request;
using DAL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICartService
    {
        Task<BaseResponse> AddToCartAsync(string userId, AddToCartRequest request);
        Task<CartSummaryResponse> GetUserCartAsync(string userId, string lan= "en");
        Task<BaseResponse> ClearCartAsync(string userId);
    }
}
