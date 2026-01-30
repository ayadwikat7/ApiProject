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
            var cartItem = await _cartRepository.GetCartItemAsync(userId, request.ProductId);

            var existingProduct = cartItem?.Count??0;
            if (product.Quantity < (request.Count+existingProduct))
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
        public async Task<BaseResponse> UpdateQuantity(string userId, int productId,int count) {

            var cartItems = await _cartRepository.GetCartItemAsync(userId,productId);

            var product = await _repository.FindByIdAsync(productId);
            if (count <= 0)
            {

                return new BaseResponse
                {
                    Message = "invalid quantity",
                    Success = false
                };
            }
            if (product.Quantity<count) {

                return new BaseResponse
                {
                    Message = "not enough stack",
                    Success =false
                };
            }
            cartItems.Count = count;
            await _cartRepository.UpdateAsync(cartItems);
            return new BaseResponse
            {
                Message = "cart updated successfully",
                Success = true
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
        public async Task<BaseResponse> RemoveFromeCartAsync(string userId,int productId) {
            var cartItem = await _cartRepository.GetCartItemAsync(userId, productId);
            if (cartItem is null) {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Product not found in cart"
                };
            }
            await _cartRepository.DeleteFromCartAsync(cartItem);
            return new BaseResponse
            {
                Success = true,
                Message = "Product removed from cart successfully"
            };

        }
    }
}
