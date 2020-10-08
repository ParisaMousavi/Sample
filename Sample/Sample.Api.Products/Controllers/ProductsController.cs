using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Products.Controllers
{

    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider _productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this._productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync() 
        {
            var result = await _productsProvider.GetProductsAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(Guid id)
        {
            var product = await _productsProvider.GetProductAsync(id);

            if (product.IsSuccess)
            {
                return Ok(product.Product);
            }

            return NotFound();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateProductAsync(Models.Product product)
        {
            var savedProduct = await _productsProvider.UpdateProductAsync(product);

            return Ok(savedProduct);
            //return RedirectToAction("Details", new { id = savedProduct.Product.Id }); // for this output the mehod output must be Task<RedirectToActionResult>
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            var result = await _productsProvider.DeleteProductAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync(Models.Product product)
        {

            // add product
            //----------------------------------------------
            var result = await _productsProvider.AddProductAsync(product);
            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(new JsonResult(result.Product));
        }

    }
}
