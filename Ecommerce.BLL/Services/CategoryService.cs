using Ecommerce.DAL.DTO.Requests;
using Ecommerce.DAL.DTO.Responses;
using Ecommerce.DAL.Models;
using Ecommerce.DAL.Repositery;
using Mapster;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Services
{
    public class CategoryService
    {
        private readonly CategoryRepositery _categoryRepositery;
        public CategoryService(CategoryRepositery categoryRepositery)
        {
            _categoryRepositery = categoryRepositery;
        }

        public IEnumerable<CategoryResponse> GetAll() => _categoryRepositery.GetAll().Adapt<IEnumerable<CategoryResponse>>();


        public CategoryResponse GetById(int id) => _categoryRepositery.GetById(id).Adapt<CategoryResponse>();

        public bool CreateCategory(CategoryRequest categoryRequest) => _categoryRepositery.CreateCategory(categoryRequest.Adapt<Category>());

        public bool delete(int id) => _categoryRepositery.delete(id);

        public bool Update(int id, CategoryRequest categoryRequest) => _categoryRepositery.Update(id, categoryRequest.Adapt<Category>());

    }
}
