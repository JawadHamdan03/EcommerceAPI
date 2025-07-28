using Ecommerce.BLL.Services;
using Ecommerce.DAL.DTO.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll()=> Ok(_categoryService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) => Ok(_categoryService.GetById(id));

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryRequest categoryRequest)=>Ok(_categoryService.CreateCategory(categoryRequest));

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) => Ok(_categoryService.delete(id));

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, CategoryRequest categoryRequest) => Ok(_categoryService.Update(id,categoryRequest));

    }
}
