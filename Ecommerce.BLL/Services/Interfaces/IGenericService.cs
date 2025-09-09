using Ecommerce.DAL.DTO.Requests;
using Ecommerce.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Services.Interfaces;

public interface IGenericService<TRequest,TResponse,Entity>
{
    IEnumerable<TResponse> GetAll();

    TResponse GetById(int id);

    bool Create(TRequest request);
    bool delete(int id);

    bool Update(int id, TRequest request);

}
