
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RadioManager.Application.Common.Notifications;
using RadioManager.Application.Time;
using RadioManager.Infrastructure.Exceptions;
using RadioManager.Infrastructure.Exceptions.Generators;
using RadioManager.Infrastructure.Exceptions.Mappers;
using RadioManager.Infrastructure.Logging;
using RadioManager.Infrastructure.Notifications;
using RadioManager.Infrastructure.Persistance;
using RadioManager.Infrastructure.Time;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RadioManager.Api")]
namespace RadioManager.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration, ConfigureHostBuilder builder)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddDal(configuration);
            services.AddLogging(builder);

            services.AddScoped<IClock, Clock>();
            services.AddScoped<ExceptionHandlerMiddleware>();
            services.AddSingleton<IExceptionToErrorMapper, ExceptionToErrorMapper>();
            services.AddScoped<INotificationSender, EmailNotificationSender>();

            services.AddSingleton<IErrorResponseGenerator, JsonErrorResponseGenerator>();

            return services;
        }

        public static void UseInfrastructure(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.MapControllers();
        }
    }
}
