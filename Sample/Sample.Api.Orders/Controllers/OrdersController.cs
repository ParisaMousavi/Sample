using Microsoft.AspNetCore.Mvc;
using Sample.Api.Orders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders") ]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _ordersProvider;

        public OrdersController(Interfaces.IOrdersProvider ordersProvider)
        {
            this._ordersProvider = ordersProvider;
        }

        public async Task<IActionResult> GetOrdersAsync()
        {
            return NotFound();
        }

        public async Task<IActionResult> GetOrderAsync()
        {
            return NotFound();
        }

        public async Task<IActionResult> AddOrderAsync()
        {
            return NotFound();
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
