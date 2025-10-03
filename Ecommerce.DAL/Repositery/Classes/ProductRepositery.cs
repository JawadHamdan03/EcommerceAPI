using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
using Ecommerce.DAL.Repositery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositery.Classes;

public class ProductRepositery : GenericRepositery<Product>, IProductRepositery
{
    public ProductRepositery(AppDbcontext _dbcontext) : base(_dbcontext)
    {
    }
}
