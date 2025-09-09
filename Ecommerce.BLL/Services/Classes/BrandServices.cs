using Ecommerce.BLL.Services.Interfaces;
using Ecommerce.DAL.DTO.Requests;
using Ecommerce.DAL.DTO.Responses;
using Ecommerce.DAL.Models;
using Ecommerce.DAL.Repositery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Services.Classes
{
    public class BrandService : GenericService<BrandRequest, BrandResponse, Brand>,IBrandService
    {
        public BrandService(IBrandRepositery repositery) : base(repositery)
        {
        }
    }
}
