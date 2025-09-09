using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
using Ecommerce.DAL.Repositery.Classes;
using Ecommerce.DAL.Repositery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositery
{
    public class CategoryRepositery : GenericRepositery<Category>, ICategoryRepositery
    {
        public CategoryRepositery(AppDbcontext _dbcontext) : base(_dbcontext)
        {

        }
    }
}
