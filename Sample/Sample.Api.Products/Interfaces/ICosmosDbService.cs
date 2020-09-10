using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Products.Interfaces
{
    public interface ICosmosDbService
    {

        Task<(bool IsSuccess, T result, string ErrorMessage)> AddProductAsync<T>(T product);

    }
}
