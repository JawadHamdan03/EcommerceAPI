using Ecommerce.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class BrandController : ControllerBase
    {

        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            this._brandService = brandService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_brandService.GetAll());


        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) => Ok(_brandService.GetById(id));



    }
}
