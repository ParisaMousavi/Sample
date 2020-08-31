using Microsoft.AspNetCore.Mvc;
using Sample.Api.Catalog.Db;
using Sample.Api.Catalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Catalog.Controllers
{
    [ApiController]
    [Route("api/catalog")]
    public class CatalogsController : ControllerBase
    {
        private readonly ICatalogsService catalogsService;
        

        public CatalogsController(Interfaces.ICatalogsService catalogsService)
        {
            this.catalogsService = catalogsService;
        
        }
        public async Task<IActionResult> AddCatalogAsync()
        {


            return NotFound();
        }

    }
}
