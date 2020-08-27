using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Products.Interfaces
{
    /// <summary>
    /// Interface contains all the methods that are called from Controllers.
    /// No business logic is allowed to be defined in Controller directly.
    /// Business logic is implemented in Providers only.
    /// Provider is concret implementation of interface.
    /// Provider fetches db Entity and return model.
    /// </summary>
    public interface IProductsProvider
    {

        Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductAsync();
        Task<(bool IsSuccess, string ErrorMessage)> AddProductAsync();

    }
}
