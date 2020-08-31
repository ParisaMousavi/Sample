using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Catalog.Db
{
    public class CatalogsDbContext : DbContext
    {
        public DbSet<Db.Catalog> Catalogs { get; set; }
        public DbSet<Db.CatalogType> catalogTypes { get; set; }

        public DbSet<Db.CatalogBrand> catalogBrands { get; set; }

        public CatalogsDbContext(DbContextOptions options) : base (options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // this method must be used to add seeding step
        }
    }
}
