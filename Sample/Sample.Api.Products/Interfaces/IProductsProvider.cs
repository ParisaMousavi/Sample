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



        Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(Guid id);
        Task<(bool IsSuccess, string ErrorMessage)> AddProductAsync(Models.Product product);
        Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> UpdateProductAsync(Models.Product product);

        Task<(bool IsSuccess, string ErrorMessage)> DeleteProductAsync(Guid id);
    }
}
