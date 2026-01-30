using DAL.DTOs.Response;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IOrderServise
    {

        Task<List<OrderResponse>> GetOrderAsync(OrderStatus orderStatus);
        Task<BaseResponse> UpdateOrderStatusAsync(int OrderId, OrderStatus orderStatusnew);
    
        Task<Order?> GetOrderByIdAsync(int orderId);

    }
}
