using Ecommerce.BLL.Services.Interfaces;
using Ecommerce.DAL.Repositery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ecommerce.DAL.Repositery.Classes;
using Ecommerce.DAL.Models;
using Mapster;
namespace Ecommerce.BLL.Services.Classes;


public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity>
    where TEntity : BaseModel
{
    private readonly IGenericRepositery<TEntity> _repositery;

    public GenericService(IGenericRepositery<TEntity> repositery)
    {
        _repositery = repositery;
    }

    public bool Create(TRequest request)
    {
        var target = request.Adapt<TEntity>();
       return _repositery.Create(target);
    }

    public bool delete(int id)=> _repositery.delete(id);
    

    IEnumerable<TResponse> GetAll()
    {
        var target = _repositery.GetAll().Adapt<IEnumerable<TResponse>>();
        return target;
    }

    public TResponse GetById(int id)
    {
        var target = _repositery.GetById(id).Adapt<TResponse>();
        return target;
        
    }

    public bool Update(int id, TRequest request)
    {
        var target = request.Adapt<TEntity>();
        return _repositery.Update(id, target);
    }

    IEnumerable<TResponse> IGenericService<TRequest, TResponse, TEntity>.GetAll()
    {
        return GetAll();
    }
}
