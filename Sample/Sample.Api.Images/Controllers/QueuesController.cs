using Microsoft.AspNetCore.Mvc;
using Sample.Api.Images.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Images.Controllers
{
    [ApiController]
    [Route("api/queues")]
    public class QueuesController : ControllerBase
    {
        private readonly IQueuesProvider _queuesProvider;

        public QueuesController(Interfaces.IQueuesProvider queuesProvider)
        {
            this._queuesProvider = queuesProvider;
        }


        [HttpGet]
        public async Task<IActionResult> GetQueuesAsync()
        {
            var queues = await _queuesProvider.ListQueues();
            return Ok(queues);

        }


        [HttpPost("{id}")]
        public async Task<IActionResult> AddToQueueAsync(int id)
        {
            var result = await _queuesProvider.AddToQueueAsync(Guid.NewGuid(), "parisa");
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
