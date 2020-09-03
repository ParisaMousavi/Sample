using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Api.Products;

namespace Sample.Api.Products
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<Interfaces.IProductsProvider, Providers.ProductsProvider>();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<Db.ProductsDbContext>(options =>
            {
                options.UseInMemoryDatabase("Products"); // Specify tha name of the database
            });
            services.AddScoped<Sample.Api.Images.Interfaces.IImagesProvider, Sample.Api.Images.Providers.ImagesProvider>();
            services.AddScoped<Sample.Api.Images.Interfaces.IQueuesProvider, Sample.Api.Images.Providers.QueueProvider>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
