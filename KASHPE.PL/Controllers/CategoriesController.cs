using DAL.Data;
using KASHPE.PL.Resoureses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHPE.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase


    {

        private readonly ApplicationDpContext _Context;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public CategoriesController(ApplicationDpContext context, IStringLocalizer<SharedResources> localizer)
        {
            _Context = context;
            _localizer = localizer;
        }
        [HttpGet("")]
        public IActionResult index()
        {
            return Ok(_localizer["Success"]);

        }
    }
}