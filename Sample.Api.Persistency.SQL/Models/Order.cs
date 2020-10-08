using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Persistency.SQL.Models
{
    public class Order
    {
        public Guid OrderGroupId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Total { get; set; }

        public List<Models.OrderItem> Items { get; set; }
    }
}
