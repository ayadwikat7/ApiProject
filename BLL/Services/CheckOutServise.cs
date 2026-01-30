using DAL.Data;
using DAL.DTOs.Request;
using DAL.DTOs.Response;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CheckOutServise : ICheckOutServise
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IOderItemsRepository _oderItemsRepository;
        private readonly IproductsRepository _productsRepository;
        private readonly ApplicationDpContext _context;

        public CheckOutServise(ICartRepository cartRepository,
            IOrderRepository orderRepository,
            IEmailSender emailSender,
            UserManager<ApplicationUsers> userManager,
            IOderItemsRepository oderItemsRepository,
            IproductsRepository productsRepository
            
            )
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
            _userManager = userManager;
            _oderItemsRepository = oderItemsRepository;
            _productsRepository = productsRepository;
            
        }
        public async Task<CheckOutResponse> ProsessPayment(CheckOutRequest request, string UserId)
        {
            var CartItems = await _cartRepository.GetUserCartItems(UserId);
            if (!CartItems.Any())
            {
                return new CheckOutResponse
                {
                    Success = false,
                    Message = "Cart is empty"

                };
            }
            decimal TotalPrice = 0;
            foreach (var cart in CartItems)
            {
                if (cart.Product.Quantity < cart.Count)
                {

                    return new CheckOutResponse
                    {
                        Success = false,
                        Message = $"Product {cart.Product.Quantity} is out of stock"
                    }
            ;
                }
                TotalPrice += cart.Product.Price * cart.Count;
            }
            Order order = new Order
            {

                UserId = UserId,
                PaymentMethod = request.PaymentMethod,
                AmountPayed = TotalPrice,
                PaiedStatus=PaiedStatus.UnPaied
            };
            if (request.PaymentMethod == PaymentMethod.Cash) {

                return new CheckOutResponse
                {


                    Success = true,
                    Message = "Order placed successfully",
                    PaymentId = Guid.NewGuid().ToString()
                };
            }
            else if (request.PaymentMethod == PaymentMethod.Visa)
            {
                if ( !CartItems.Any())
                {
                    return new CheckOutResponse
                    {
                        Success = false,
                        Message = "Cart is empty"
                    };
                }

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    Mode = "payment",
                    SuccessUrl = "http://localhost:5070/api/checkout/success?session_id={CHECKOUT_SESSION_ID}",

                    CancelUrl = "http://localhost:5070/checkout/cancel",
                    Metadata = new Dictionary<string, string> {
                        {  "UserId",UserId }
                   },


                    LineItems = new List<SessionLineItemOptions>()
                };

                foreach (var item in CartItems)
                {
                    var productName =
                        item.Product?.ProductTransulations?
                            .FirstOrDefault(t => t.Language == "en")?.Name
                        ?? item.Product?.ProductTransulations?.FirstOrDefault()?.Name
                        ?? "Product";

                    var unitAmount = (long)(item.Product.Price * 100);

                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = productName
                            },
                            UnitAmount = unitAmount
                        },
                        Quantity = item.Count
                    });
                }

                var service = new SessionService();
                var session = await service.CreateAsync(options);
                order.SeessionId= session.Id;
                order.PaiedStatus = PaiedStatus.Paid;
             await   _orderRepository.CreateAsync(order);
                return new CheckOutResponse
                {
                    Success = true,
                    Message = "payment session created",
                    Url = session.Url
                };
            }
            return new CheckOutResponse
            {
                Success = false,
                Message = "unsupported payment method"
            };

        }
       

        public async Task<CheckOutResponse> HandleSuccessAsync(string sessionId)
        {
            var service = new SessionService();
            var session = service.Get(sessionId);
            var userId = session.Metadata["UserId"];

            var order = await _orderRepository.GetBySessionIdAsync(sessionId);

            order.PaymentIntentId = session.PaymentIntentId;
            order.OrderStatus = OrderStatus.Approved;

            await _orderRepository.UpdateAsync(order);

            var user = await _userManager.FindByIdAsync(userId);

            var cartItems = await _cartRepository.GetUserCartItems(userId);
            var orderItems = new List<OrderItem>();
            var ProductsUpdate = new List<(int protectedid,int quantity)>();

            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    UnitPrice = cartItem.Product.Price,
                    Quantity = cartItem.Count,
                    TotalPrice = cartItem.Product.Price * cartItem.Count,

                };

                orderItems.Add(orderItem);
                ProductsUpdate.Add((cartItem.ProductId, cartItem.Count));
            }
            await _oderItemsRepository.AddRangeAsync(orderItems);
            await _cartRepository.ClearCartAsync(userId);
            await _emailSender.SendEmailAsync(
                user.Email,
                "Payment successful",
                "<h2>Thank you for your order</h2>"
            );

            return new CheckOutResponse
            {
                Success = true,
                Message = "Payment completed successfully"
            };
        }


    }
}
