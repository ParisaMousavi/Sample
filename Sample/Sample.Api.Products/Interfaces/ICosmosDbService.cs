using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Products.Interfaces
{
    public interface  ICosmosDbService
    {

        Task<(bool IsSuccess , string ErrorMessage)> AddProductAsync();

    }
}
