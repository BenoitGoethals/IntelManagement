using IntelVault.ApplicationCore.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using IntelVault.Infrastructure.repos;
using IntelVault.ApplicationCore.Services;

namespace IntelVault.Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run(); Console.WriteLine("Hello, World!");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    // Configure MongoDB

                  
                   
                    services.AddSingleton<IMongoClient>(sp =>
                    {
                       var setting= new MongoClientSettings()
                        {
                            Scheme = ConnectionStringScheme.MongoDB,
                            Server = new MongoServerAddress("localhost", 27017),
                            Credential = MongoCredential.CreateCredential("IntelVault", "benoit", "ranger14")
                        };
                        return new MongoClient(setting);
                    });

                    services.AddScoped<MongoDbRepository<HumInt>>();
                    services.AddScoped<IntelService<HumInt>>();

                    // Configure NLog for logging
                    services.AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders(); // Clear other logging providers
                        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                        loggingBuilder.AddNLog(hostContext.Configuration);
                    });
                });
    }
}