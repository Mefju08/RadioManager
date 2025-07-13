using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using RadioManager.Application.Shows.Commands.Create;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RadioManager.Api")]
namespace RadioManager.Application
{
    internal static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(x =>
                x.RegisterServicesFromAssembly(typeof(CreateShowCommand).Assembly));

            services.AddValidatorsFromAssemblyContaining<CreateShowCommand>()
                 .AddFluentValidationAutoValidation();


            return services;
        }
    }
}
