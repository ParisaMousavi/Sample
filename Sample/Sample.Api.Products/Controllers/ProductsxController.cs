using Microsoft.AspNetCore.Mvc;
using Sample.Api.Images.Interfaces;
using Sample.Api.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Products.Controllers
{
    public class ProductsxController : ControllerBase
    {
        private readonly IProductsProvider _productsProvider;
        private readonly IImagesProvider _imageProvider;

        public ProductsxController(IProductsProvider productsProvider, IImagesProvider imageProvider)
        {
            this._productsProvider = productsProvider;
            this._imageProvider = imageProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index() //Get All
        {
            var products = await _productsProvider.GetProductsAsync();

            return Ok(products);
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

        [HttpPost("{product}")]
        public async Task<IActionResult> Edit(Models.Product product)
        {
            var savedProduct = await _productsProvider.UpdateProductAsync(product);

            return Ok(savedProduct);
            //return RedirectToAction("Details", new { id = savedProduct.Product.Id }); // for this output the mehod output must be Task<RedirectToActionResult>
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productsProvider.DeleteProductAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result.ErrorMessage);
        }


        public async Task<IActionResult> Add(Models.Product product)
        {
            product.Id = Guid.NewGuid();
            var filepath = product.ImageUrl;
            var fileExtension = new System.IO.FileInfo(filepath).Extension;
            var blobName = $"{product.Id}.{fileExtension}";

            var uploadedImage = await  _imageProvider.UploadBlobAsync(filepath, blobName);

            if (!uploadedImage.IsSuccess)
            {
                return NotFound(uploadedImage.ErrorMessage);
            }

            var result = await _productsProvider.AddProductAsync(product);
            if (result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result);
            
        }

    }
}
