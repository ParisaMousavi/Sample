using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Configure CosmosDB Service 
            var docClient = new DocumentClient(
                new Uri(Configuration.GetValue<string>("DBUri")),
                Configuration.GetValue<string>("DBKey"));
            services.AddSingleton<DocumentClient>(docClient);

            services.AddScoped<Interfaces.IProductsProvider, Services.ProductsProvider>();
            services.AddScoped<Interfaces.IImagesService, Services.ImagesService>();
            services.AddScoped<Interfaces.ICosmosDbService, Services.CosmosDbService>();

            services.AddAutoMapper(typeof(Startup));

            //---------------------------------------------------------------
            // In memory database
            //---------------------------------------------------------------
            // https://docs.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/data-driven-crud-microservice
            // Microsoft.EntityFrameworkCore.SqlServer 3.1.7/3.1.6
            services.AddDbContext<Db.ProductsDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetValue<string>("ConnectionString"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(
                        typeof(Startup).GetTypeInfo().Assembly.GetName().Name);

                    //Configuring Connection Resiliency:
                    sqlOptions.
                        EnableRetryOnFailure(maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);

                });

                // Changing default behavior when client evaluation occurs to throw.
                // Default in EFCore would be to log warning when client evaluation is done.
                options.ConfigureWarnings(warnings => warnings.Throw(
                    RelationalEventId.QueryClientEvaluationWarning));
            });



            //---------------------------------------------------------------
            // In memory database
            //---------------------------------------------------------------
            //services.AddDbContext<Db.ProductsDbContext>(options =>
            //{
            //    options.UseInMemoryDatabase("Products"); // Specify tha name of the database
            //});

            //---------------------------------------------------------------
            // Add Image Service 
            //---------------------------------------------------------------
            services.AddHttpClient("ImagesService", config =>
            {
                config.BaseAddress = new Uri(Configuration["Services:Images"]);
            })
                .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 2, durationOfBreak: TimeSpan.FromMinutes(2)));
            // Adding Resilience and Transient Fault handling to your .NET Core HttpClient with Polly
            

            //---------------------------------------------------------------
            // Add SQL Persistency Service 
            //---------------------------------------------------------------
            services.AddHttpClient("SQLPersistencyService", config =>
            {
                config.BaseAddress = new Uri(Configuration["Services:SQL"]);
            })
                .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 2, durationOfBreak: TimeSpan.FromMinutes(2)));
            // Adding Resilience and Transient Fault handling to your .NET Core HttpClient with Polly


            services.AddControllers();


            //---------------------------------------------------------------
            // Add Swagger 
            //---------------------------------------------------------------
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

            // Swagger middleware
            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Project"); });

            app.UseRouting();

            // CORS Policy middleware (order is important Routing-Cors-Authorization)
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
