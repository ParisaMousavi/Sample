﻿using AutoMapper;
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

        public OrdersProvider(Db.OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._mapper = mapper;
        }

        async Task<(bool IsSuccess, string Order, string ErrorMessage)> IOrdersProvider.AddOrderAsync(Models.Order order)
        {
            try
            {

                order.Id = Guid.NewGuid();

                var ios = new List<Db.OrderItem>();
                foreach (Models.OrderItem oi in order.Items)
                {
                    ios.Add(new Db.OrderItem()
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        Name = oi.Name,
                        Price = oi.Price,
                        Quantity = oi.Quantity
                    }); ; ;
                }

                await _dbContext.Orders.AddAsync(new Db.Order()
                {
                    Id = order.Id,
                    Fullname = order.Fullname,
                    OrderDate = order.OrderDate,
                    Items = ios
                });

                await _dbContext.SaveChangesAsync();


                return (true, "Link to see the order", null);
            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        Task<(bool IsSuccess, string ErrorMessage)> IOrdersProvider.DeleteOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> IOrdersProvider.GetOrderAsync(Guid id)
        {
            try
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
                var orderItems =  _dbContext.OrderItems.Where(oi => oi.OrderId == order.Id);
                if (order != null)
                {
                    var result = _mapper.Map<Models.Order>(order);
                    var resultoi = _mapper.Map<IEnumerable< Models.OrderItem>>(orderItems);
                    result.Items = (ICollection<Models.OrderItem>) resultoi;
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

        async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> IOrdersProvider.GetOrdersAsync()
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
