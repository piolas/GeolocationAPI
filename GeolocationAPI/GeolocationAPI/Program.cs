using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace GeolocationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            try
            {
                Log.Information("Starting up");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
             .ConfigureAppConfiguration((context, config) =>
             {

                 config.AddJsonFile("azurekeyvault.json", optional: false, reloadOnChange: true);
                 var root = config.Build();
                 config.AddAzureKeyVault(
                    $"https://{root["azureKeyVault:vault"]}.vault.azure.net/",
                    root["azureKeyVault:clientId"],
                    root["azureKeyVault:clientSecret"]);
             })
                    .UseSerilog()
             .UseStartup<Startup>();
    }
}
