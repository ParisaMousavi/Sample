using Sample.Api.Catalog.Interfaces;
using Sample.Api.Catalog.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Catalog.Services
{
    public class CatalogsService : Interfaces.ICatalogsService
    {
        Task<(bool IsSuccess, int Id, string ErrorMessage)> ICatalogsService.AddCatalogAsync(CatalogAddTerms catalogAddTerms)
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, string ErrorMessage)> ICatalogsService.DeleteCatalogAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, string ErrorMessage)> ICatalogsService.UpdateCatalogAsync(CatalogUpdateTerms catalogUpdateTerms)
        {
            throw new NotImplementedException();
        }
    }
}
