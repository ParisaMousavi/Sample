using Microsoft.AspNetCore.Cors;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderAsync(Guid id)
        {
            var result = await _ordersProvider.GetOrderAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Order);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderAsync([FromBody]Models.Order order)
        {
            var result = await _ordersProvider.AddOrderAsync(order);

            if (result.IsSuccess)
            {
                return Ok(result.Order);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateOrderAsync(Models.Order order )
        {
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(Guid id)
        {
            return NotFound();
        }

    }
}
