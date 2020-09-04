using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;
using Sample.Api.Images.Extensions;
using Sample.Api.Images.Interfaces;

namespace Sample.Api.Images.Providers
{
    public class ImagesProvider : IImagesProvider
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly QueueServiceClient _queueServiceClient;
        private readonly ILogger _logger;

        public ImagesProvider(BlobServiceClient blobServiceClient, QueueServiceClient queueServiceClient, ILogger<ImagesProvider> logger)
        {
            this._blobServiceClient = blobServiceClient;
            this._queueServiceClient = queueServiceClient;
            this._logger = logger;
        }

        async Task<(bool IsSuccess, string ThumbnailUrl, string ErrorMessage)> IImagesProvider.CreateThumbnailAsync(string filePath, string blobName)
        {
            throw new NotImplementedException();
        }

        async Task<(bool IsSuccess, Stream Content, string ContentType, string ErrorString)> IImagesProvider.GetBlobAsync(string name)
        {
            try
            {

                // 1. Container Client
                var containerClient = _blobServiceClient.GetBlobContainerClient("products");
                // 2. Blob Client
                var blobClient = containerClient.GetBlobClient(name);

                BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

                return (true, blobDownloadInfo.Content, blobDownloadInfo.ContentType, null);

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, null, ex.Message);
            }
        }

        async Task<(bool IsSuccess, IEnumerable<string> Blobs, string ErrorMessage)> IImagesProvider.ListBlobAsync()
        {
            try
            {
                // 1. Container Client
                var containerClient = _blobServiceClient.GetBlobContainerClient("products");

                var items = new List<string>();

                await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
                {
                    items.Add(blobItem.Name);
                }

                return (true, items, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }

        async Task<(bool IsSuccess, string ImageUrl, string ErrorMessage)> IImagesProvider.UploadBlobAsync(string filePath, string blobName)
        {

            // 1. Container Client
            var containerClient = _blobServiceClient.GetBlobContainerClient("products");

            // 2. Blob Client
            var blobClient = containerClient.GetBlobClient(blobName);

            using FileStream uploadFileStream = File.OpenRead(filePath);

            var blobContentInfo = await blobClient.UploadAsync(uploadFileStream, true);

            uploadFileStream.Close();

            return (true, blobContentInfo.Value.ToString(), null);
        }

    }
}
