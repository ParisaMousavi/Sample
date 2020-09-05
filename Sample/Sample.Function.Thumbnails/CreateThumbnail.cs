using System;
using System.IO;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Sample.Function.Thumbnails
{
    public static class CreateThumbnail
    {

        private static readonly string BLOB_STORAGE_CONNECTION_STRING = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

        [FunctionName("CreateThumbnail")]
        public static void Run(
            [QueueTrigger("products", Connection = "")]Terms.QueueMessage queueMessage,
            [Blob("productsthumbnail", FileAccess.Write, Connection = "AzureWebJobsStorage")] CloudBlobContainer smallImageContainer,
            ILogger log)
        {


            log.LogInformation($"C# Queue trigger function processed:{queueMessage.ImageName}");


            var thumbnailWidth = Convert.ToInt32(Environment.GetEnvironmentVariable("THUMBNAIL_WIDTH"));
            var thumbContainerName = Environment.GetEnvironmentVariable("THUMBNAIL_CONTAINER_NAME");
            var blobServiceClient = new BlobServiceClient(BLOB_STORAGE_CONNECTION_STRING);

            var sourceBlobContainerClient = blobServiceClient.GetBlobContainerClient("products");

            var originalImage = sourceBlobContainerClient.GetBlobClient(queueMessage.ImageName);
            var smallImageBlob = smallImageContainer.GetBlockBlobReference(queueMessage.ImageName);

            // read originl blob to a memory stream
            var output = new MemoryStream();
            originalImage.DownloadTo(output);

            output.Position = 0;
            using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>(output, new PngDecoder()))
            {

                var divisor = image.Width / thumbnailWidth;
                var height = Convert.ToInt32(Math.Round((decimal)(image.Height / divisor)));

                image.Mutate(x => x.Resize(thumbnailWidth, height));
                image.Save(output, new PngEncoder());
                smallImageBlob.UploadFromStreamAsync(output);
                
            }

        }

    }
}
