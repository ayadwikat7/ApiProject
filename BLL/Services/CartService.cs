using DAL.DTOs.Request;
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
    public class CartService : ICartService
    {
        private readonly IproductsRepository _repository;
        private readonly ICartRepository _cartRepository;

        public CartService(IproductsRepository repository,ICartRepository cartRepository)
        {
            _repository = repository;
            _cartRepository = cartRepository;
        }
        public async Task<BaseResponse> AddToCartAsync(string userId, AddToCartRequest request)
        {
            var product = await _repository.FindByIdAsync(request.ProductId);
            if (product.Quantity < request.Count)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = $"Quantity should be less than {product.Quantity}"
                };

            }


            if (product is null) {

                return new BaseResponse {

                    Message = "Product not found",
                    Success = false

                };

        }
            var cartItem = await _cartRepository.GetCartItemAsync(userId, request.ProductId);
            if (cartItem is not null)
            {
                cartItem.Count += request.Count;
                await _cartRepository.UpdateAsync(cartItem);
                return new BaseResponse
                {
                    Message = "Product added to cart successfully",
                    Success = true
                };
            }
            else {
                var Cart = request.Adapt<Cart>();
                Cart.UserId = userId;
                await _cartRepository.AddCartItemAsy(Cart);
                return new BaseResponse
                {
                    Message = "Product added to cart successfully",
                    Success = true
                };
            }
           
        }

        public async Task<CartSummaryResponse> GetUserCartAsync(string userId, string lan = "en")
        {
            var cartItems = await _cartRepository.GetUserCartItems(userId);

            var items = cartItems
                .Select(c => new CartResponse
                {
                    ProductId = c.ProductId,
                    ProductName = c.Product.ProductTransulations
                                       .FirstOrDefault(t => t.Language == lan)?.Name,
                    Count = c.Count,
                    Price = c.Product.Price
                })
                .ToList();

            return new CartSummaryResponse
            {
                Carts = items
            };

        }

        public async Task<BaseResponse> ClearCartAsync(string userId)
        {
            await _cartRepository.ClearCartAsync(userId);

            return new BaseResponse
            {
                Success = true,
                Message = "cart cleared successfully"
            };
        }

    }
}
