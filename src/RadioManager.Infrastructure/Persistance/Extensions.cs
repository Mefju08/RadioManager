using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RadioManager.Application.Common;
using RadioManager.Domain.Repositories;
using RadioManager.Infrastructure.Persistance.Options;
using RadioManager.Infrastructure.Persistance.Repositories;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RadioManager.Api")]
namespace RadioManager.Infrastructure.Persistance
{
    internal static class Extensions
    {
        public static IServiceCollection AddDal(this IServiceCollection services,
            IConfiguration configuration)
        {
            var sqlServerOptions = configuration
                 .GetRequiredSection(SqlServerOptions.SectionName)
                 .Get<SqlServerOptions>();

            services.AddDbContext<RadioManagerDbContext>(options =>
            {
                options.UseSqlServer(sqlServerOptions.ConnectionString);
                //options.UseInMemoryDatabase("radiomanager-db");
            });

            services.AddScoped<IShowRepository, ShowRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddHostedService<DbInitializer>();

            return services;
        }
    }
}
