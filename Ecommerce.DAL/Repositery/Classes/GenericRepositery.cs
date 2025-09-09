using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
using Ecommerce.DAL.Repositery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositery.Classes;

public class GenericRepositery<T> : IGenericRepositery<T> where T :BaseModel
{
    private readonly AppDbcontext _dbcontext;

    public GenericRepositery(AppDbcontext _dbcontext)
    {
        this._dbcontext = _dbcontext;
    }
    public bool Create(T entity)
    {
        if(entity is not null)
        {
            _dbcontext.Set<T>().Add(entity);
            _dbcontext.SaveChanges();
            return true;
        }
        return false;
    }  

    public bool delete(int id)
    {
        if(id>0)
        {
            var target = _dbcontext.Set<T>().FirstOrDefault(x => x.Id==id);
            _dbcontext.Set<T>().Remove(target);
            _dbcontext.SaveChanges();
            return true;
        }
        return false;
    }

    public IEnumerable<T> GetAll()  => _dbcontext.Set<T>().ToList();
  

    public T GetById(int id) =>_dbcontext.Set<T>().Find(id);


    public bool Update(int id, T entity)
    {
        if(entity is not null)
        {entity.Id = id;

        _dbcontext.Update(entity);
            return true;
        }
        return false;
    }
}
