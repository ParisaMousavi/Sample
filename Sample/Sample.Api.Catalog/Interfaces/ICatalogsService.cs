using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Catalog.Interfaces
{
    public interface ICatalogsService
    {
        public Task<(bool IsSuccess, int Id ,string ErrorMessage)> AddCatalogAsync(Terms.CatalogAddTerms catalogAddTerms);
        public Task<(bool IsSuccess, string ErrorMessage)> UpdateCatalogAsync(Terms.CatalogUpdateTerms catalogUpdateTerms);
        public Task<(bool IsSuccess, string ErrorMessage)> DeleteCatalogAsync(int id);
    }
}
