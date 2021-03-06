﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.Api.Products.Db;
using Sample.Api.Products.Interfaces;
using Sample.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Products.Services
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
        private readonly ProductsDbContext _dbContext;
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IImagesService _imagesService;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper _mapper;

        public ProductsProvider(Db.ProductsDbContext dbContext, Interfaces.ICosmosDbService cosmosDbService, Interfaces.IImagesService imagesService, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._cosmosDbService = cosmosDbService;
            this._imagesService = imagesService;
            this._logger = logger;
            this._mapper = mapper;

            //SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Products.Any())
            {
                _dbContext.Products.Add(new Db.Product() { Id = Guid.NewGuid(), Name = "Book", Price = 29, Inventory = 100 });
                _dbContext.Products.Add(new Db.Product() { Id = Guid.NewGuid(), Name = "Car", Price = 29000, Inventory = 3 });
                _dbContext.Products.Add(new Db.Product() { Id = Guid.NewGuid(), Name = "Lamp", Price = 12, Inventory = 450 });
                _dbContext.SaveChanges();
            }

        }

        async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> IProductsProvider.GetProductsAsync()
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> IProductsProvider.AddProductAsync(Models.Product product)
        {
            try
            {
                // Assign a unique ID toproduct
                product.Id = Guid.NewGuid();


                // upload original image of the product
                // upload adds a message to queue
                var resultUploadedImage = await _imagesService.UploadBlobAsync(product.Id, product.ImageUrl);
                if (!resultUploadedImage.IsSuccess)
                {
                    throw new Exception(resultUploadedImage.ErrorMessage);
                }

                // add product
                product.ImageUrl = resultUploadedImage.ImageUrl;
                _dbContext.Products.Add(new Db.Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Inventory = product.Inventory,
                    ImageUrl = resultUploadedImage.ImageUrl
                });
                _dbContext.SaveChanges();

                var newProduct = await _cosmosDbService.AddProductAsync<Models.Product>(product);
                if (!newProduct.IsSuccess) throw new Exception(newProduct.ErrorMessage);

                return (true, (dynamic)newProduct.result, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }


        async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> IProductsProvider.GetProductAsync(Guid id)
        {
            try
            {
                var product = _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if(product != null)
                {
                    var result = _mapper.Map<Models.Product>(product);
                    return (true, null , null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
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
