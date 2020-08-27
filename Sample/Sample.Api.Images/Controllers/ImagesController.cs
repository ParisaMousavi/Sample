using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Api.Images.Interfaces;

namespace Sample.Api.Images.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesProvider imagesProvider;

        public ImagesController(IImagesProvider imagesProvider)
        {
            this.imagesProvider = imagesProvider;
        }

        [Route("{name}")]
        public async Task<IActionResult> GetBlobAsync(string name)
        {
            var image = await imagesProvider.GetBlobAsync(name);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetImagesAsync()
        {
            return Ok(await imagesProvider.ListBlobAsync());
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadBlobAsync([FromBody] Terms.Upload request)
        {
            await imagesProvider.UploadBlobAsync(request.FilePath, request.FileName);
            return Ok();
        }

    }
}
