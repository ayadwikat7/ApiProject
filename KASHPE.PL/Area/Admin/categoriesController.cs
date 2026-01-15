using BLL.Services;
using DAL.DTOs.Request;
using KASHPE.PL.Resoureses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace KASHPE.PL.Area.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]//session 12 part3
    public class categoriesController : ControllerBase
    {
        private readonly ICategorySevices _categorySevices;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public categoriesController(
            ICategorySevices categorySevices,
            IStringLocalizer<SharedResources> stringLocalizer)
        {
            _categorySevices = categorySevices;
            _stringLocalizer = stringLocalizer;
        }
        [HttpPost("")]
        public IActionResult Create([FromBody] requestCategory requestCategory)
        {
         //   var createdBy = User.FindFirstValue(ClaimTypes.NameIdentifier);// session 12 part1
            var response = _categorySevices.CreateCategory(requestCategory);
            return Ok(new { message = _stringLocalizer["Success"].Value, response });
        }
    }
}
