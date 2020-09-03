using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Products.Interfaces
{
    public interface IImagesService
    {
        public Task<(bool IsSuccess, string ImageUrl, string ErrorMessage)> UploadBlobAsync(string filePath, string blobName);
        public Task<(bool IsSuccess, IEnumerable<string> Blobs, string ErrirMessage)> ListBlobAsync();
    }
}
