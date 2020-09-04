using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Images.Interfaces
{
    public interface IImagesProvider
    {

        public Task<(bool IsSuccess, Stream Content, string ContentType, string ErrorString)> GetBlobAsync(string name);
        public Task<(bool IsSuccess, string ImageUrl, string ErrorMessage)> UploadBlobAsync(string filePath, string blobName);
        public Task<(bool IsSuccess, IEnumerable<string> Blobs, string ErrorMessage)> ListBlobAsync();

        public Task<(bool IsSuccess, string ThumbnailUrl, string ErrorMessage)> CreateThumbnailAsync(string filePath, string blobName);
    }
}
