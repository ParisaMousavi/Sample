using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Sample.Api.Images.Extensions;
using Sample.Api.Images.Interfaces;

namespace Sample.Api.Images.Services
{
    public class ImagesProvider : IImagesProvider
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly ILogger logger;

        public ImagesProvider(BlobServiceClient blobServiceClient, ILogger<ImagesProvider> logger)
        {
            this.blobServiceClient = blobServiceClient;
            this.logger = logger;
        }

        async Task<(Stream Content, string ContentType)> IImagesProvider.GetBlobAsync(string name)
        {
            try
            {

                // 1. Container Client
                var containerClient = blobServiceClient.GetBlobContainerClient("products");
                // 2. Blob Client
                var blobClient = containerClient.GetBlobClient(name);

                BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

                return (blobDownloadInfo.Content, blobDownloadInfo.ContentType);

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (null, null);
            }
        }

        async Task<IEnumerable<string>> IImagesProvider.ListBlobAsync()
        {
            try
            {
                // 1. Container Client
                var containerClient = blobServiceClient.GetBlobContainerClient("products");

                var items = new List<string>();

                await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
                {
                    items.Add(blobItem.Name);
                }

                return items;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (null);
            }

        }

        async Task IImagesProvider.UploadBlobAsync(string filePath, string fileName)
        {
            filePath = @"C:\Users\P.Moosavinezhad\Pictures\MPI.png";

            // 1. Container Client
            var containerClient = blobServiceClient.GetBlobContainerClient("products");

            // 2. Blob Client
            var blobClient = containerClient.GetBlobClient(fileName);

            using FileStream uploadFileStream = File.OpenRead(filePath);

            await blobClient.UploadAsync(uploadFileStream,true);

            uploadFileStream.Close();
        }
    }
}
