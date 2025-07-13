using Microsoft.Extensions.DependencyInjection;
using RadioManager.Domain.Shows.Services;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RadioManager.Api")]
namespace RadioManager.Domain
{
    internal static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IShowSchedulingService, ShowSchedulingService>();

            return services;
        }
    }
}
