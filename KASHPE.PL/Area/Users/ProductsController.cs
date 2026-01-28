using BLL.Services;
using KASHPE.PL.Resoureses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHPE.PL.Area.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _ProductsSevices;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ProductsController(
            IProductService ProductsySevices,
            IStringLocalizer<SharedResources> stringLocalizer)
        {
            _ProductsSevices = ProductsySevices;
            _stringLocalizer = stringLocalizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> index([FromQuery] string lan = "en", [FromQuery]int page = 1, [FromQuery]int limit = 3, 
    [FromQuery] string? search = null,
    [FromQuery] int? categoryId = null,
    [FromQuery] decimal? minPrice = null,
    [FromQuery] decimal? maxPrice = null,
    [FromQuery] string? sortBy = null,
    [FromQuery] bool isAscending = true

    )
        {
            var response = await _ProductsSevices.GetAllProductsForUser(
                  lan,
                  page,
                  limit,
                    search,
                  categoryId,
                  minPrice,
                  maxPrice,
                  sortBy,
                    isAscending

              ); return Ok(new
            {
                message = _stringLocalizer["Success"].Value,
                response
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetalies([FromRoute]int id,[FromQuery] string lan = "en")
        {
            var response = await _ProductsSevices.GetAllProductsDetailsForUser(id,lan);
            return Ok(new
            {
                message = _stringLocalizer["Success"].Value,
                response
            });
        }
    }
}
