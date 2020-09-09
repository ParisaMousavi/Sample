using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Polly;

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

            services.AddSingleton(x => new CosmosClient(Configuration.GetValue<string>("CosmosDbConnectionString")));


            services.AddScoped<Interfaces.IProductsProvider, Services.ProductsProvider>();
            services.AddScoped<Interfaces.IImagesService, Services.ImagesService>();

            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<Db.ProductsDbContext>(options =>
            {
                options.UseInMemoryDatabase("Products"); // Specify tha name of the database
            });

            //services.AddDbContextPool<Db.ProductsDbContext>(
            //    options => options.UseSqlServer(Configuration.GetConnectionString("ProductsDbConnection"))
            //    );

            // Adding Resilience and Transient Fault handling to your .NET Core HttpClient with Polly
            services.AddHttpClient("ImagesService", config =>
            {
                config.BaseAddress = new Uri(Configuration["Services:Images"]);
            })
                .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 2, durationOfBreak: TimeSpan.FromMinutes(2)));
            //.AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.RetryAsync(2));
            //.AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample Project", Version = "1.0", Description = "Product API" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Project"); });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
