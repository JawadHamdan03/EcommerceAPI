using Ecommerce.DAL.DTO.Requests;
using Ecommerce.DAL.DTO.Responses;
using Ecommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Services.Interfaces
{
    public interface IProductService : IGenericService<ProductRequest,ProductReposne,Product>
    {
        Task<bool> CreateFileAsync(ProductRequest request);
    }
}
