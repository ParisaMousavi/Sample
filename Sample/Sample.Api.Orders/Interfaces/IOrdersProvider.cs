using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Orders.Interfaces
{
    public interface  IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync();
        Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrderAsync(Guid id);
        Task<(bool IsSuccess, string Order, string ErrorMessage)> AddOrderAsync(Models.Order order);
        Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> UpdateOrderAsync(Models.Order order);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteOrderAsync(Guid id);
    }
}
