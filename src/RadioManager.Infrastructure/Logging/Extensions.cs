
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RadioManager.Infrastructure.Exceptions;
using Serilog;
using Serilog.Filters;

namespace RadioManager.Infrastructure.Logging
{
    internal static class Extensions
    {
        public static void AddLogging(this IServiceCollection services, ConfigureHostBuilder builder)
        {
            services.AddSingleton<IErrorLoggingStrategy, DefaultErrorLoggingStrategy>();
            services.AddSingleton<IErrorLoggingStrategy, BusinessErrorLoggingStrategy>();
                  
            builder.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(Matching.FromSource<BusinessErrorCategory>())
                    .WriteTo.File(
                        path: "error_logs.txt",
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] ERROR: {Message:lj}{NewLine}{Exception}",
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error))
                .WriteTo.Logger(lc => lc
                    .Filter.ByExcluding(Matching.FromSource<BusinessErrorCategory>())
                    .WriteTo.File(
                        path: "general_logs.txt",
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] {Message:lj}{NewLine}{Exception}",
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)));
        }
    }
}
