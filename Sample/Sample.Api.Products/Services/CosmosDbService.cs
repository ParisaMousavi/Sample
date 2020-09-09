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
		private readonly ILogger _logger;

		public CosmosDbService(ILogger logger)
		{
			this._logger = logger;
		}

        async Task<(bool IsSuccess, string ErrorMessage)> ICosmosDbService.AddProductAsync()
        {
			try
			{


			}
			catch (Exception ex)
			{

				_logger?.LogError(ex.Message);
				return (false, ex.Message);
			}
        }
    }
}
