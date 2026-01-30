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
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository,IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<BaseResponse> AddReviewAsync(string UserId, CreateReviewRequest reviewRequest,int ProductId)
        {
            var reviewResponse =await _orderRepository.HasUserDeliverdOrderForProduct(UserId, ProductId);
            if (!reviewResponse) {
                return new BaseResponse
                {
                    Message = "You can only review products youhave recives",
                    Success = false

                };
            }
            var alreadyReviewed = await _reviewRepository.HasUserReviewProducts(UserId, ProductId);
            if (alreadyReviewed)
            {
                return new BaseResponse
                {
                    Message = "You have already reviewed this product.",
                    Success = false
                };
            }
            var review = reviewRequest.Adapt<Review>();
            review.UserId = UserId;
            review.ProductId= ProductId;
            return new BaseResponse
            {
                Message = "Your review has been added successfully.",
                Success = true

            };
        }
    }
}
