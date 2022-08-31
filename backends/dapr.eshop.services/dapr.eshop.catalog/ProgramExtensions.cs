using Dapr.Client;
using Dapr.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Polly;
using Serilog;
using dapr.eshop.catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace dapr.eshop.catalog
{
    public static class ProgramExtensions
    {
        private const string AppName = "Catalog API";

        public static void AddCustomConfiguration(this WebApplicationBuilder builder)
        {
            // Disabled temporarily until https://github.com/dapr/dotnet-sdk/issues/779 is resolved.
            builder.Configuration.AddDaprSecretStore(
                "secretstore",
                new DaprClientBuilder().Build());
        }

        public static void AddCustomSerilog(this WebApplicationBuilder builder)
        {
            var seqServerUrl = builder.Configuration["SeqServerUrl"];

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.Console()
                .WriteTo.Seq(seqServerUrl)
                .Enrich.WithProperty("ApplicationName", AppName)
                .CreateLogger();

            builder.Host.UseSerilog();
        }

        public static void AddCustomHealthChecks(this WebApplicationBuilder builder) =>
        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddDapr()
            .AddCosmosDb(
                builder.Configuration["ConnectionStrings:CatalogDB"],
                name: "CatalogDB-check",
                tags: new string[] { "catalogdb" });

        public static void AddCustomSwagger(this WebApplicationBuilder builder) =>
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"eShopOnDapr - {AppName}", Version = "v1" });
            });

        public static void UseCustomSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppName} V1");
            });
        }

        public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
        {
            //builder.Services.AddScoped<IEventBus, DaprEventBus>();
            //builder.Services.AddScoped<OrderStatusChangedToAwaitingStockValidationIntegrationEventHandler>();
            //builder.Services.AddScoped<OrderStatusChangedToPaidIntegrationEventHandler>();
        }

        public static void AddCustomDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<CatalogDbContext>(options => 
                    options.UseCosmos(builder.Configuration["ConnectionStrings:CatalogDB"], "catalogs"));
        }


        private static Policy CreateRetryPolicy(IConfiguration configuration, Serilog.ILogger logger)
        {
            // Only use a retry policy if configured to do so.
            // When running in an orchestrator/K8s, it will take care of restarting failed services.
            if (bool.TryParse(configuration["RetryMigrations"], out bool retryMigrations))
            {
                return Policy.Handle<Exception>().
                    WaitAndRetryForever(
                        sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                        onRetry: (exception, retry, timeSpan) =>
                        {
                            logger.Warning(
                                exception,
                                "Exception {ExceptionType} with message {Message} detected during database migration (retry attempt {retry}, connection {connection})",
                                exception.GetType().Name,
                                exception.Message,
                                retry,
                                configuration["ConnectionStrings:CatalogDB"]);
                        }
                    );
            }

            return Policy.NoOp();
        }
    }
}
