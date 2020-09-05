using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Function.Thumbnails.Terms
{
   public class QueueMessage
    {
        public Guid ProductId { get; set; }

        public string ContainerName { get; set; }

        public string ImageName { get; set; }

    }
}
