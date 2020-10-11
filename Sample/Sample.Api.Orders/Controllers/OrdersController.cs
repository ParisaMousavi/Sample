using Microsoft.AspNetCore.Mvc;
using Sample.Api.Orders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _ordersProvider;

        public OrdersController(Interfaces.IOrdersProvider ordersProvider)
        {
            this._ordersProvider = ordersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var result = await _ordersProvider.GetOrdersAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }

        public async Task<IActionResult> GetOrderAsync()
        {
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderAsync(Models.Order order)
        {
            var result = await _ordersProvider.AddOrderAsync(order);

            if (result.IsSuccess)
            {
                return Ok(result.Order);
            }
            return NotFound(result.ErrorMessage);
        }

        public async Task<IActionResult> UpdateOrderAsync()
        {
            return NotFound();
        }

        public async Task<IActionResult> DeleteOrderAsync()
        {
            return NotFound();
        }

    }
}
