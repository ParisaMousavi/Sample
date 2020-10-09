using Sample.Api.Orders.Interfaces;
using Sample.Api.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Orders.Services
{
    public class ProductProvider : Interfaces.IOrdersProvider
    {
        Task<(bool IsSuccess, Order Order, string ErrorMessage)> IOrdersProvider.AddOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, string ErrorMessage)> IOrdersProvider.DeleteOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, Order Order, string ErrorMessage)> IOrdersProvider.GetOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, List<Order> Orders, string ErrorMessage)> IOrdersProvider.GetOrdersAsync()
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, Order Order, string ErrorMessage)> IOrdersProvider.UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
