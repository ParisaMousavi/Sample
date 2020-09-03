using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Products.Models
{
    public class Messages
    {
        public Guid ProductId { get; set; }

        public string ContainerName { get; set; }

        public string ImageName { get; set; }
    }
}
