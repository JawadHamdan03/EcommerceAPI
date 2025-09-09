using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
using Ecommerce.DAL.Repositery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositery.Classes;

public class BrandRepositery : GenericRepositery<Brand>, IBrandRepositery
{
    public BrandRepositery(AppDbcontext _dbcontext) : base(_dbcontext)
    {
    }
}
