using DAL.DTOs.Response;
using DAL.Models;
using DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderServise : IOrderServise
    {
        private readonly IOrderRepository _orderRepository;

        public OrderServise(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderResponse>> GetOrderAsync(OrderStatus orderStatus)
        {
            var orders = await _orderRepository.GetOrderByStatuesAsync(orderStatus);
            return orders.Adapt<List<OrderResponse>>();
        }

        public Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<BaseResponse> UpdateOrderStatusAsync(int OrderId, OrderStatus orderStatusnew)
        {
            var order =await _orderRepository.GetOrderByIdAsync(OrderId);
            order.OrderStatus = orderStatusnew;
            if (orderStatusnew == OrderStatus.Delivered) { 
                order.PaiedStatus = PaiedStatus.Paid;
            }
            //else if(orderStatusnew == OrderStatus.Cancelled)
            //{
            //    if (orderStatusnew == OrderStatus.Shipped) { 
            //        return new BaseResponse
            //        {
            //            Message = "Cannot cancel an order that has already been shipped.",
            //            Success = false
                       
            //        };
            //    }
            //}
            await _orderRepository.UpdateAsync(order);
            return new BaseResponse
            {
                Message = "Order status updated successfully.",
                Success = true
            };

        }
    }
}
