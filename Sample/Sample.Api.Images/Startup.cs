using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sample.Api.Images
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
            //-------------CORS Policy------------------------------
            // https://www.infoworld.com/article/3327562/how-to-enable-cors-in-aspnet-core.html
            // https://docs.microsoft.com/de-de/aspnet/core/security/cors?view=aspnetcore-3.1
            services.AddCors(options => {
                options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin());
            });
            //-------------------------------------------

            services.AddSingleton(x => new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorageConnectionString")));
            services.AddSingleton(x => new QueueServiceClient(Configuration.GetValue<string>("AzureBlobStorageConnectionString")));
            services.AddScoped<Interfaces.IImagesProvider, Providers.ImagesProvider>();
            services.AddScoped<Interfaces.IQueuesProvider, Providers.QueueProvider>();
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
