using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Orders.Profiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            //CreateMap<Db.Order, Models.Order>()
            //    .ForMember(des => des.Items, opt =>
            //    opt.MapFrom(source => source.Items.Where(w => w.OrderId == source.Id).Select(w => w).ToList()));
            CreateMap<Db.Order, Models.Order>();
            CreateMap<Db.OrderItem , Models.OrderItem >();

        }
    }
}
