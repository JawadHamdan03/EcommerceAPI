using Ecommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositery.Interfaces
{
    public interface IGenericRepositery<T> where T : BaseModel
    {
       IEnumerable<T> GetAll();
       T GetById(int id);
       bool Create(T entity);
       bool delete(int id);
       bool Update(int id, T entity);
       
    }
}
