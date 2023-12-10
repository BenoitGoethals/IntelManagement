using FluentValidation;
using FluentValidation.AspNetCore;
using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.Infrastructure.repos;
using Microsoft.AspNetCore.Authentication.Negotiate;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using NLog.Extensions.Logging;
using IntelVault.ApplicationCore.validation;

namespace IntelVault.IntelApi
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddSingleton<IMongoClient,MongoClient>(sp =>
            //{
            //    var setting = new MongoClientSettings()
            //    {
            //        Scheme = ConnectionStringScheme.MongoDB,
            //        Server = new MongoServerAddress("192.168.0.130", 27017),
            //        Credential = MongoCredential.CreateCredential("IntelVault", "benoit", "ranger14")
            //    };
            //    return new MongoClient(setting);
            //});


            builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var setting = new MongoClientSettings()
                {
                    Scheme = ConnectionStringScheme.MongoDB,
                    Server = new MongoServerAddress("localhost", 27017),
                  //  Credential = MongoCredential.CreateCredential("IntelVault", "benoit", "ranger14")
                };
                return new MongoClient(setting);
            });

            builder.Services.AddValidatorsFromAssemblyContaining<HumIntValidator>();
            builder.Services.AddSingleton<IMongoDbRepository<SocialMedia>, MongoDbRepository<SocialMedia>>(n => new MongoDbRepository<SocialMedia>(n.GetRequiredService<IMongoClient>(), n.GetRequiredService<ILogger<IMongoDbRepository<SocialMedia>>>(), "IntelVault"));
            builder.Services.AddSingleton<IMongoDbRepository<CybInt>, MongoDbRepository<CybInt>>(n => new MongoDbRepository<CybInt>(n.GetRequiredService<IMongoClient>(), n.GetRequiredService<ILogger<IMongoDbRepository<CybInt>>>(), "IntelVault"));
            builder.Services.AddSingleton<IMongoDbRepository<HumInt>, MongoDbRepository<HumInt>>(n => new MongoDbRepository<HumInt>(n.GetRequiredService<IMongoClient>(), n.GetRequiredService<ILogger<IMongoDbRepository<HumInt>>>(), "IntelVault"));
            builder.Services.AddSingleton<IMongoDbRepository<PersonOfInterest>, MongoDbRepository<PersonOfInterest>>(n => new MongoDbRepository<PersonOfInterest>(n.GetRequiredService<IMongoClient>(), n.GetRequiredService<ILogger<IMongoDbRepository<PersonOfInterest>>>(), "IntelVault"));


            builder.Services.AddScoped<IIntelService<PersonOfInterest>,IntelService<PersonOfInterest>>(n=>new IntelService<PersonOfInterest>(n.GetRequiredService<IMongoDbRepository<PersonOfInterest>>(), n.GetRequiredService<PersonOfInterestValidator>()));
            builder.Services.AddScoped<IIntelService<HumInt>, IntelService<HumInt>>(n => new IntelService<HumInt>(n.GetRequiredService<IMongoDbRepository<HumInt>>(), n.GetRequiredService<HumIntValidator>()));
            builder.Services.AddScoped<IIntelService<CybInt>, IntelService<CybInt>>(n => new IntelService<CybInt>(n.GetRequiredService<IMongoDbRepository<CybInt>>(), n.GetRequiredService<CybIntValidator>()));

            builder.Services.AddScoped<IIntelService<SocialMedia>, IntelService<SocialMedia>>(n => new IntelService<SocialMedia>(n.GetRequiredService<IMongoDbRepository<SocialMedia>>(), n.GetRequiredService<SocialMediaValidator>()));
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            
            // Configure NLog for logging
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders(); // Clear other logging providers
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddNLog(builder.Configuration);
                loggingBuilder.AddConsole();
            });

            builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                .AddNegotiate();

            builder.Services.AddAuthorization(options =>
            {
                // By default, all incoming requests will be authorized according to the default policy.
                options.FallbackPolicy = options.DefaultPolicy;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Enable CORS (Cross-Origin Resource Sharing)
            app.UseCors(corsPolicyBuilder =>
                corsPolicyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
            app.UseHttpsRedirection();

          //  app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
