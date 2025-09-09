using Ecommerce.BLL.Services.Classes;
using Ecommerce.BLL.Services.Interfaces;
using Ecommerce.DAL.DTO.Requests;
using Ecommerce.DAL.DTO.Responses;
using Ecommerce.DAL.Models;
using Ecommerce.DAL.Repositery.Classes;
using Ecommerce.DAL.Repositery.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Services
{
    public class CategoryService : GenericService<CategoryRequest, CategoryResponse, Category>, ICategoryService
    {
        public CategoryService(ICategoryRepositery repositery) : base(repositery)
        {
        }
    }
}
