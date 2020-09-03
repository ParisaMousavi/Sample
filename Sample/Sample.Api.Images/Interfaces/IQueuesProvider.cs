using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Images.Interfaces
{
    public interface IQueuesProvider
    {
        Task<(bool IsSuccess, string ErrorMessage)> AddToQueueAsync(Guid productId, string blobName);
        Task<(bool IsSuccess, IEnumerable<string > Queues,  string ErrorMessage)> ListQueues();

    }
}
