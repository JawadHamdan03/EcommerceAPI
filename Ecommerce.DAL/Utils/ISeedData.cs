using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Utils
{
    public interface ISeedData
    {

        Task SeedDataModelsAsync();
        Task IdentitySeedDataAsync();
        

    }
}
