using Microsoft.Extensions.Logging;
using Sample.Api.Products.Interfaces;
using Sample.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sample.Api.Products.Services
{
    public class OrdersService : Interfaces.IOrdersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrdersService> _logger;

        public OrdersService(IHttpClientFactory httpClientFactory, ILogger<OrdersService> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._logger = logger;
        }

        Task<(bool IsSuccess, Order Order, string ErrorMessage)> IOrdersService.AddOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, string ErrorMessage)> IOrdersService.DeleteOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task<(bool IsSuccess, Order Order, string ErrorMessage)> IOrdersService.GetOrderAsync(Guid id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OrdersService");
                var response = await client.GetAsync($"api/orders/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Order>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }

        async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> IOrdersService.GetOrdersAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OrdersService");
                var response = await client.GetAsync("api/orders");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        Task<(bool IsSuccess, Order Order, string ErrorMessage)> IOrdersService.UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
