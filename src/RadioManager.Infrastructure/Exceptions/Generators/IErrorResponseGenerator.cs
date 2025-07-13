using Microsoft.AspNetCore.Http;
using RadioManager.Infrastructure.Exceptions.Models;

namespace RadioManager.Infrastructure.Exceptions.Generators
{
    internal interface IErrorResponseGenerator
    {
        Task GenerateResponseAsync(HttpContext context, Error error);
    }
}
