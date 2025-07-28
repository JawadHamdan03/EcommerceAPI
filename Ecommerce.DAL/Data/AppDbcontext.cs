using Ecommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Data
{
    public class AppDbcontext : DbContext
    {

        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
            
        }


        public DbSet<Category> Categories { get; set; }

    }
}
