using BLL.Services;
using DAL.DTOs.Request;
using DAL.DTOs.Response;
using KASHPE.PL.Resoureses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace KASHPE.PL.Area.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public CartsController(
            ICartService cartService,
            IStringLocalizer<SharedResources> stringLocalizer)
        {
            _cartService = cartService;
            _stringLocalizer = stringLocalizer;
        }
        [HttpPost("")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest cartRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.AddToCartAsync(userId, cartRequest);
            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> index([FromQuery] string lan = "en")
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.GetUserCartAsync(userId, lan);
            return Ok(result);
        }
        [HttpDelete("")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _cartService.ClearCartAsync(userId);

            return Ok(result);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DelteCartItem([FromRoute] int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _cartService.RemoveFromeCartAsync(userId,productId);

            return Ok(result);
        }

        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateQuatity([FromRoute] int productId, [FromBody] UpdateQuatityReqest updateQuatity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _cartService.UpdateQuantity(userId, productId, updateQuatity.Count);
            if(!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
