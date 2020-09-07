using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NuGet.Configuration;

namespace Sample.Api.Products
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //// pull in the environment variable configuration
            //var environmentConfiguration = new ConfigurationBuilder()
            //    .AddEnvironmentVariables()
            //    .Build();
            //var environment = environmentConfiguration["RUNTIME_ENVIRONMENT"];


            //// load the app settings into configuration
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", false, true)
            //    .AddJsonFile($"appsettings.{environment}.json", true, true)
            //    .AddEnvironmentVariables()
            //    .AddCommandLine(args)
            //    .Build();

            //// parse all settings into the settings class structure
            //var settings = configuration.Get<Settings>();

            //if (!environment.Equals("local", StringComparison.OrdinalIgnoreCase))
            //{
            //    var azureServiceTokenProvider = new AzureServiceTokenProvider();
            //    var keyVaultClient = new KeyVaultClient(
            //        new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            //    configuration = new ConfigurationBuilder()
            //        .AddConfiguration(configuration)
            //        .AddAzureKeyVault(settings.AppSettings.KeyVaultSettings.DnsName,keyVaultClient, new DefaultKeyVaultSecretManager())
            //        .Build();

            //    settings = configuration.Get<Settings>();
            //}

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
