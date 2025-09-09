using Ecommerce.BLL.Services.Classes;
using Ecommerce.BLL.Services.Interfaces;
using Ecommerce.DAL.DTO.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll()=> Ok(_categoryService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) => Ok(_categoryService.GetById(id));

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryRequest categoryRequest)=>Ok(_categoryService.Create(categoryRequest));

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) => Ok(_categoryService.delete(id));

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, CategoryRequest categoryRequest) => Ok(_categoryService.Update(id,categoryRequest));

    }
}
