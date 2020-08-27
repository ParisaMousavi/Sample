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

        public Task<(Stream Content, string ContentType)> GetBlobAsync(string name);
        public Task UploadBlobAsync(string filePath, string fileName);
        public Task<IEnumerable<string>> ListBlobAsync();

    }
}
