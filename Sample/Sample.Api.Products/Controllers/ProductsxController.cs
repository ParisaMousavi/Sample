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
    [Route("api/productsx")]
    public class ProductsxController : ControllerBase
    {
        private readonly IProductsProvider _productsProvider;

        public ProductsxController(IProductsProvider productsProvider)
        {
            this._productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index() //Get All
        {
            var result = await _productsProvider.GetProductsAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound();

            //return View(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid id) //Get one Product
        {
            var product = await _productsProvider.GetProductAsync(id);

            if (product.IsSuccess)
            {
                return Ok(product.Product);
            }

            return NotFound(product.ErrorMessage);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(Models.Product product)
        {
            var savedProduct = await _productsProvider.UpdateProductAsync(product);

            return Ok(savedProduct);
            //return RedirectToAction("Details", new { id = savedProduct.Product.Id }); // for this output the mehod output must be Task<RedirectToActionResult>
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productsProvider.DeleteProductAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Models.Product product)
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
