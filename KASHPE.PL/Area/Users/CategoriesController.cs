using BLL.Services;
using DAL.DTOs.Request;
using KASHPE.PL.Resoureses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHPE.PL.Area.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategorySevices _categorySevices;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public CategoriesController(
            ICategorySevices categorySevices,
            IStringLocalizer<SharedResources> stringLocalizer)
        {
            _categorySevices = categorySevices;
            _stringLocalizer = stringLocalizer;
        }

        [HttpGet("")] 
        public IActionResult index() {
            var response = _categorySevices.GetAllCategory();
            return Ok(new { message = _stringLocalizer["Success"].Value, 
                response }); 
        }

        
    }
   
}
