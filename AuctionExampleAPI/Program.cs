using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionExampleAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuctionExampleAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var auctionExampleContext = services.GetRequiredService<AuctionExampleContext>();
                await AuctionExampleContextSeed.SeedAsync(auctionExampleContext);
            }
            catch (Exception ex)
            {
                // TODO: Add Logger and Exceptions
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
