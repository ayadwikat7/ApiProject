using BLL.Services;
using DAL.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KASHPE.PL.Area.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CheckOutController : ControllerBase
    {
        private readonly ICheckOutServise _checkOutServise;

        public CheckOutController(ICheckOutServise checkOutServise)
        {
            _checkOutServise = checkOutServise;
        }
        [HttpPost("")]
        public async Task<IActionResult> Payment([FromBody] CheckOutRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _checkOutServise.ProsessPayment(request, userId);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [AllowAnonymous]
   
        [HttpGet("success")]
        public async Task<IActionResult> Success([FromQuery(Name = "session_id")] string sessionId)
        {
            var response = await _checkOutServise.HandleSuccessAsync(sessionId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(new
            {
                message = "Payment Success",
                paymentId = response.PaymentId,   
            });
        }

    }
}
