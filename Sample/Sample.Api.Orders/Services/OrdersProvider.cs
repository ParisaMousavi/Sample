using AutoMapper;
using Microsoft.Extensions.Logging;
using Sample.Api.Orders.Db;
using Sample.Api.Orders.Interfaces;
using Sample.Api.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Orders.Services
{
    public class OrdersProvider : Interfaces.IOrdersProvider
    {
        private readonly OrdersDbContext _ordersDbContext;
        private readonly ILogger<OrdersProvider> _logger;
        private readonly IMapper _mapper;

        public OrdersProvider(Db.OrdersDbContext ordersDbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this._ordersDbContext = ordersDbContext;
            this._logger = logger;
            this._mapper = mapper;
        }

        Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> IOrdersProvider.AddOrderAsync(Models.Order order)
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, string ErrorMessage)> IOrdersProvider.DeleteOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> IOrdersProvider.GetOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> IOrdersProvider.GetOrdersAsync()
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> IOrdersProvider.UpdateOrderAsync(Models.Order order)
        {
            throw new NotImplementedException();
        }
    }
}
