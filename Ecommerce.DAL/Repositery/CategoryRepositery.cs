using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositery
{
    public class CategoryRepositery
    {
        private readonly AppDbcontext context;
        public CategoryRepositery(AppDbcontext context)
        {
            this.context = context;
        }
        public IEnumerable<Category> GetAll()=> context.Categories.ToList();

        public Category GetById(int id) => context.Categories.Find(id);
        
        public bool CreateCategory(Category category)  
        {
            if (category is not null)
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return true;
            }
            else
                return false;

        }

        public bool delete(int id)
        {
            var category = context.Categories.FirstOrDefault(c=> c.Id==id);
            if (category is not null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
        
        public bool Update(int id , Category category)
        {
            if (category is not null)
            {
                category.Id = id;
                context.Categories.Update(category);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
