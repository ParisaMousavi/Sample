using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using Sample.Api.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Products.Services
{
    public class CosmosDbService : Interfaces.ICosmosDbService
    {
        private readonly DocumentClient _documentClient;
        private readonly ILogger _logger;
        private readonly Uri _productCollectionUri;

        public CosmosDbService(DocumentClient documentClient, ILogger<CosmosDbService> logger)
        {
            this._documentClient = documentClient;
            this._logger = logger;
            _productCollectionUri = UriFactory.CreateDocumentCollectionUri("Products", "products");
        }

        //async Task<(bool IsSuccess, string ErrorMessage)> ICosmosDbService.AddProductAsync(Models.Product product)
        //{
        //    try
        //    {
        //        var dbResponse = await _documentClient.CreateDocumentAsync(_productCollectionUri, product);
        //        var result = (dynamic)dbResponse.Resource;
        //        return (true, null);

        //    }
        //    catch (Exception ex)
        //    {

        //        _logger?.LogError(ex.Message);
        //        return (false, ex.Message);
        //    }
        //}

        async Task<(bool IsSuccess, T result, string ErrorMessage)> ICosmosDbService.AddProductAsync<T>(T product)
        {
            try
            {
                var dbResponse = await _documentClient.CreateDocumentAsync(_productCollectionUri, product);
                var result = (dynamic)dbResponse.Resource;
                return (true, result, null);

            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.Message);
                return (false, default(T), ex.Message);
            }
        }
    }
}
