using FluentValidation;
using FluentValidation.AspNetCore;
using IntelVault.ApplicationCore.IntelData;
using IntelVault.Infrastructure;
using IntelVault.WebAccess.Components;
using IntelVault.WebAccess.Components.Account;
using IntelVault.WebAccess.Data;
using MatBlazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using NLog.Extensions.Logging;
using Syncfusion.Blazor;
using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.ApplicationCore.validation;
using IntelVault.Infrastructure.repos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;
var builder = WebApplication.CreateBuilder(args);
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8 / V1NHaF1cWGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEZiWH1dcXZURmBUUEd2Wg ==");
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();


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
builder.Services.AddSingleton<IMongoDbRepository<PersonOfInterest>, MongoDbRepository<IntelVault.ApplicationCore.Model.PersonOfInterest>>(n => new MongoDbRepository<PersonOfInterest>(n.GetRequiredService<IMongoClient>(), n.GetRequiredService<ILogger<IMongoDbRepository<PersonOfInterest>>>(), "IntelVault"));
builder.Services.AddSingleton<IMongoDbRepository<GeneralIntel>, MongoDbRepository<GeneralIntel>>(n => new MongoDbRepository<GeneralIntel>(n.GetRequiredService<IMongoClient>(), n.GetRequiredService<ILogger<IMongoDbRepository<GeneralIntel>>>(), "IntelVault"));
builder.Services.AddSingleton<IMongoDbRepository<OpenSourceInt>, MongoDbRepository<OpenSourceInt>>(n => new MongoDbRepository<OpenSourceInt>(n.GetRequiredService<IMongoClient>(), n.GetRequiredService<ILogger<IMongoDbRepository<OpenSourceInt>>>(), "IntelVault"));
builder.Services.AddSingleton<IMongoDbRepository<IntelDocument>, MongoDbRepository<IntelDocument>>(n => new MongoDbRepository<IntelDocument>(n.GetRequiredService<IMongoClient>(), n.GetRequiredService<ILogger<IMongoDbRepository<IntelDocument>>>(), "IntelVault"));
builder.Services.AddSingleton<IMongoDbRepository<IntelInvestigationFile>, MongoDbRepository<IntelInvestigationFile>>(n => new MongoDbRepository<IntelInvestigationFile>(n.GetRequiredService<IMongoClient>(), n.GetRequiredService<ILogger<IMongoDbRepository<IntelInvestigationFile>>>(), "IntelVault"));

builder.Services.AddScoped<IDocumentService, DocumentService>(n => new DocumentService(mongodbDbRepository: n.GetRequiredService<IMongoDbRepository<IntelDocument>>(), n.GetRequiredService<IntelDocumentValidator>()));
builder.Services.AddScoped<IIntelService<PersonOfInterest>, IntelService<PersonOfInterest>>(n => new IntelService<PersonOfInterest>(n.GetRequiredService<IMongoDbRepository<PersonOfInterest>>(), n.GetRequiredService<PersonOfInterestValidator>()));
builder.Services.AddScoped<IIntelService<HumInt>, IntelService<HumInt>>(n => new IntelService<HumInt>(n.GetRequiredService<IMongoDbRepository<HumInt>>(), n.GetRequiredService<HumIntValidator>()));
builder.Services.AddScoped<IIntelService<CybInt>, IntelService<CybInt>>(n => new IntelService<CybInt>(n.GetRequiredService<IMongoDbRepository<CybInt>>(), n.GetRequiredService<CybIntValidator>()));
builder.Services.AddScoped<IIntelService<IntelInvestigationFile>, IntelService<IntelInvestigationFile>>(n => new IntelService<IntelInvestigationFile>(n.GetRequiredService<IMongoDbRepository<IntelInvestigationFile>>(), n.GetRequiredService<IntelInvestigationFileValidator>()));

builder.Services.AddScoped<IIntelService<GeneralIntel>, IntelService<GeneralIntel>>(n => new IntelService<GeneralIntel>(n.GetRequiredService<IMongoDbRepository<GeneralIntel>>(), n.GetRequiredService<GeneralIntelValidator>()));
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddSingleton<ServiceCountry>();
builder.Services.AddScoped<IIntelService<SocialMedia>, IntelService<SocialMedia>>(n => new IntelService<SocialMedia>(n.GetRequiredService<IMongoDbRepository<SocialMedia>>(), n.GetRequiredService<SocialMediaValidator>()));
builder.Services.AddScoped<IIntelService<OpenSourceInt>, IntelService<OpenSourceInt>>(n => new IntelService<OpenSourceInt>(n.GetRequiredService<IMongoDbRepository<OpenSourceInt>>(), n.GetRequiredService<OpenSourceIntelValidator>()));





builder.Services.AddScoped<IGlobalService, GlobalService>(x => new GlobalService(x.GetRequiredService<IIntelService<SocialMedia>>()
    , x.GetRequiredService<IIntelService<PersonOfInterest>>(), x.GetRequiredService<IIntelService<HumInt>>(),
    x.GetRequiredService<IIntelService<CybInt>>(), x.GetRequiredService<IIntelService<GeneralIntel>>(),
    x.GetRequiredService<IIntelService<OpenSourceInt>>()));




builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddMatBlazor();
// Configure NLog for logging
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders(); // Clear other logging providers
    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
    loggingBuilder.AddNLog(builder.Configuration);
    loggingBuilder.AddConsole();
});
builder.Services.AddServerSideBlazor().AddCircuitOptions(x => x.DetailedErrors = true); ;
builder.Services.AddSyncfusionBlazor();
var app = builder.Build();
BsonClassMap.RegisterClassMap<SocialMedia>(cm =>
{
    cm.AutoMap();
    cm.SetDiscriminator("SocialMedia");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();

