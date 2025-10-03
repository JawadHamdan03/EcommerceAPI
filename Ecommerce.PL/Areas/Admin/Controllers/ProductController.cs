using Ecommerce.BLL.Services.Interfaces;
using Ecommerce.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService ProductService)
        {
            this._productService = ProductService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] ProductRequest request)
        {
            var result = await _productService.CreateFileAsync(request);
            return Ok(result);
        }

    }
}
