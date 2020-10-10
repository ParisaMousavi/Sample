using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Orders.Db
{
    public class OrdersDbContext : DbContext
    {

        public DbSet<Db.Order> Orders { get; set; }

        public OrdersDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
