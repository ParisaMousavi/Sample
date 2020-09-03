using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sample.Api.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sample.Api.Products.Providers
{
    public class ImagesService : Interfaces.IImagesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ImagesService> _logger;

        public ImagesService(IHttpClientFactory httpClientFactory, ILogger<ImagesService> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._logger = logger;
        }

        async Task<(bool IsSuccess, IEnumerable<string> Blobs, string ErrirMessage)> IImagesService.ListBlobAsync()
        {
            throw new NotImplementedException();
        }

        async Task<(bool IsSuccess, string ImageUrl, string ErrorMessage)> IImagesService.UploadBlobAsync(string filePath, string blobName)
        {
            try
            {
                var uploadTerms = new Terms.Upload ();
                uploadTerms.FilePath  = filePath;
                uploadTerms.FileName   = blobName ;

                var json = JsonConvert.SerializeObject(uploadTerms );
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var client = _httpClientFactory.CreateClient("ImagesService");
                var response = await client.PostAsync($"api/images/upload", data);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                }
                return (true, ":)", null);

            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
