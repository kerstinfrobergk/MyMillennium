using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using MyMillenniumApi.Services;
using MyMillenniumApi.DataAccess;

namespace MyMillennium
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactClient", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:61587")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

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

            builder.Services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();

                var connectionString =
                    config["AzureServiceBus:ConnectionString"]
                    ?? throw new InvalidOperationException(
                        "Azure Service Bus connection string is missing.");

                return new ServiceBusClient(connectionString);
            });

            builder.Services.AddSingleton<ServiceBusService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors("ReactClient");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
