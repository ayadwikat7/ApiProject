using BLL.Services;
using DAL.DTOs.Request;
using KASHPE.PL.Resoureses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHPE.PL.Area.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ProductsContoller : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ProductsContoller(IProductService service , IStringLocalizer<SharedResources> localizer)
        {
            _service = service;
            _localizer = localizer;
        }
        [HttpPost("")]
       public async Task<IActionResult> Create([FromForm] ProductsRequest request)
{
    var response = await _service.CreateProduct(request);

    return Ok(new
    {
        message = _localizer["Success"].Value,
        response = response
    });
}
        [HttpGet("")]
        public async Task<IActionResult> index()
        {
            var response = await _service.GetAllproductsForAdmin();
            return Ok(new
            {
                message = _localizer["Success"].Value,
                response
            });
        }
    }
}
