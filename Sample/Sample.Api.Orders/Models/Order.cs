using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Orders.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Models.OrderItem> Items { get; set; }
    }
}
