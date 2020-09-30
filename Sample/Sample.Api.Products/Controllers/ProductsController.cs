using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Products.Controllers
{
    /// <summary>
    /// The methods that are defined in interfaces and implemented in providers can be called here.
    /// No business logic is allowed to be defined in controller.
    /// </summary>
    /// 

    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase // from MVC
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(Interfaces.IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await productsProvider.GetProductsAsync();
            if(result.IsSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound();
        }


    }
}
