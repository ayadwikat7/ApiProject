using DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHPE.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase


    {

        private readonly ApplicationDpContext _Context;
        public CategoriesController(ApplicationDpContext context)
        {
            _Context = context;
        }

    }
}
