using Ecommerce.BLL.Services.Interfaces;
using Ecommerce.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService _brandService)
        {
            this._brandService = _brandService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_brandService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) => Ok(_brandService.GetById(id));

        [HttpPost]
        public IActionResult Create([FromBody] BrandRequest Request) => Ok(_brandService.Create(Request));

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) => Ok(_brandService.delete(id));

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, BrandRequest Request) => Ok(_brandService.Update(id, Request));

    }
}

