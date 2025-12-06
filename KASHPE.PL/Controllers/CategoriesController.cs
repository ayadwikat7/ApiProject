using Azure;
using BLL.Services;
using DAL.Data;
using DAL.DTOs.Request;
using DAL.DTOs.Response;
using DAL.Models;
using DAL.Repository;
using KASHPE.PL.Resoureses;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace KASHPE.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICategorySevices _categorySevices;

        public CategoriesController(IStringLocalizer<SharedResources> localizer,
            ICategorySevices categorySevices)
        {   
            _localizer = localizer;
            _categorySevices = categorySevices;
        }
        [HttpGet("")]
        public IActionResult index()
        {
            var response = _categorySevices.GetAllCategory();
            return Ok(new{ _localizer["Success"].Value,response});
        }
        [HttpPost("")]
        public IActionResult Create(requestCategory request)
        {
            var response = _categorySevices.CreateCategory(request);
            return Ok(new { message=_localizer["Success"].Value });
        }
    }
}