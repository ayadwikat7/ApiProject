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
    [Authorize(Roles = "Admin")]//session 12 part3
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
    
        public async Task<IActionResult>  Create([FromBody] requestCategory requestCategory)
        {
         //   var createdBy = User.FindFirstValue(ClaimTypes.NameIdentifier);// session 12 part1
            var response = _categorySevices.CreateCategory(requestCategory);
            return Ok(new { message = _stringLocalizer["Success"].Value, response });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var response = await _categorySevices.DeleteCategory(id);

            if (!response.Success)
                return NotFound(response);   //session 13 part1

            return Ok(response);
        }

        //ssesion 13 part2

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] requestCategory request)
        {
            var response = await _categorySevices.UpdateCategory(id, request);
            if (!response.Success)
                return NotFound(response);   //session 13 part2
            return Ok(response);
        }

        [HttpPatch("taggole-statuse/{id}")]
        public async Task<IActionResult> TaggelStateuse([FromRoute] int id)
        {
            var response = await _categorySevices.TaggelStateuse(id);
            if (!response.Success)
                return NotFound(response);   //session 13 part2
            return Ok(response);
        }
        [HttpGet("")]
        public async Task<IActionResult> index()
        {
            var response = await _categorySevices.GetAllCategory();
            return Ok(new
            {
                message = _stringLocalizer["Success"].Value,
                response
            });
        }

    }

}
