﻿using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sample.Api.Images.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Api.Images.Providers
{
    public class QueueProvider : Interfaces.IQueuesProvider
    {
        private readonly QueueServiceClient _queueServiceClient;
        private readonly ILogger _logger;

        public QueueProvider(QueueServiceClient queueServiceClient, ILogger<QueueProvider> logger)
        {
            this._queueServiceClient = queueServiceClient;
            this._logger = logger;
        }


        async Task<(bool IsSuccess, string ErrorMessage)> IQueuesProvider.AddToQueueAsync(Guid productId, string blobName)
        {
            try
            {
                var thumbnailCreationMessage = new Terms.Messages()
                {
                    ProductId = productId,
                    ContainerName = "products",
                    ImageName = blobName
                };


                var json = JsonConvert.SerializeObject(thumbnailCreationMessage);
                string base64EncodedExternalAccount = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

                var queueClient = _queueServiceClient.GetQueueClient("products");
                var sendReceipt = await queueClient.SendMessageAsync(base64EncodedExternalAccount);
                return (true, null);

            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        async Task<(bool IsSuccess, IEnumerable<string> Queues, string ErrorMessage)> IQueuesProvider.ListQueues()
        {
            try
            {
                var queueClient = _queueServiceClient.GetQueueClient("products");


                // 1. Container Client
                var queues = await queueClient.ReceiveMessagesAsync(maxMessages: 1);



                var items = new List<string>();

                items.Add("hello");
                return (true, items, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
