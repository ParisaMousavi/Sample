using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Products.Profiles
{
    public class ProductProfile: AutoMapper.Profile
    {

        public ProductProfile()
        {
            CreateMap<Db.Product, Models.Product>();
        }

    }
}
