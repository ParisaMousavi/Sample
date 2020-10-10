using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly OrdersDbContext _dbContext;
        private readonly ILogger<OrdersProvider> _logger;
        private readonly IMapper _mapper;

        public OrdersProvider(Db.OrdersDbContext ordersDbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this._dbContext = ordersDbContext;
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

     async   Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> IOrdersProvider.GetOrdersAsync()
        {
            try
            {
                var orders = await _dbContext.Orders.ToListAsync();
                var orderitems = await _dbContext.OrderItems.ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> IOrdersProvider.UpdateOrderAsync(Models.Order order)
        {
            throw new NotImplementedException();
        }
    }
}
