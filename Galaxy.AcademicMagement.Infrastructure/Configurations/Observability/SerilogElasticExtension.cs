using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Galaxy.AcademicMagement.Infrastructure.Configurations.Observability
{
    public static class SerilogElasticExtension
    {
        public static void AddSerilogElastic(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticUri = configuration["ElasticConfiguration:Uri"];

            if (string.IsNullOrEmpty(elasticUri))
            {
                throw new ArgumentNullException("ElasticConfiguration:Uri", "ElasticSearch URI is not configured.");
            }

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = $"galaxy-academicmanagement-api-logs-{DateTime.UtcNow:yyyy-MM}"
                })
                .Enrich.WithProperty("Application", "Galaxy.AcademicManagement.API")
                .CreateLogger();

            services.AddLogging(logBuilder =>
            {
                logBuilder.ClearProviders();
                logBuilder.AddSerilog(dispose: true);
            });
        }
    }
}
