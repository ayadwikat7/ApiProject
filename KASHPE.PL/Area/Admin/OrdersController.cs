using BLL.Services;
using DAL.DTOs.Request;
using DAL.Models;
using KASHPE.PL.Resoureses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Stripe;

namespace KASHPE.PL.Area.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServise _orderService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public OrdersController(IOrderServise OrderService, IStringLocalizer<SharedResources> localizer)
        {
            _orderService = OrderService;
            _localizer = localizer;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderStatus orderStatus = OrderStatus.Pending)
        {
            var orders = await _orderService.GetOrderAsync(orderStatus);
            return Ok(orders);
        }
        [HttpPatch("{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] int orderId, [FromBody] UpdateOrdereStatuesRequest orderStatus)
        {
            var isUpdated = await _orderService.UpdateOrderStatusAsync(orderId, orderStatus.OrderStatus);
            if (!isUpdated.Success)
            {
                return BadRequest(_localizer["UpdateFailed"].Value);
            }
            return Ok(_localizer["UpdateSuccess"].Value);
        }
    }
}
