using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.Api.Products.Db;
using Sample.Api.Products.Interfaces;
using Sample.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Products.Providers
{
    /// <summary>
    /// Interface contains all the methods that are called from Controllers.
    /// No business logic is allowed to be defined in Controller directly.
    /// Business logic is implemented in Providers only.
    /// Provider is concret implementation of interface.
    /// Provider fetches db Entity and return model.
    /// </summary>
    public class ProductsProvider : Interfaces.IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(Db.ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = Guid.NewGuid(), Name = "Book", Price = 29, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = Guid.NewGuid(), Name = "Car", Price = 29000, Inventory = 3 });
                dbContext.Products.Add(new Db.Product() { Id = Guid.NewGuid(), Name = "Lamp", Price = 12, Inventory = 450 });
                dbContext.SaveChanges();
            }

        }

        async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> IProductsProvider.GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        async Task<(bool IsSuccess, string ErrorMessage)> IProductsProvider.AddProductAsync(Models.Product product)
        {
            try
            {
                return (true, null);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }


        Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> IProductsProvider.GetProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> IProductsProvider.UpdateProductAsync(Models.Product product)
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, string ErrorMessage)> IProductsProvider.DeleteProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
