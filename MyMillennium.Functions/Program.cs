using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyMillennium.Data.DataAccess;
using MyMillennium.Functions.Services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    var connectionString =
        configuration["AzureStorage:ConnectionString"]
        ?? throw new InvalidOperationException(
            "Azure Storage connection string is missing!");

    return new BlobServiceClient(connectionString);
});

builder.Services.AddScoped<BlobStorageService>();
builder.Services.AddScoped<ImageThumbnailService>();
builder.Services.AddScoped<ProcessArtImageService>();

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
