using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RadioManager.Infrastructure.Exceptions.Models;

namespace RadioManager.Infrastructure.Exceptions.Generators
{
    internal sealed class JsonErrorResponseGenerator : IErrorResponseGenerator
    {
        public async Task GenerateResponseAsync(HttpContext context, Error error)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)error.HttpCode;
            var errorTxt = JsonConvert.SerializeObject(error);
            await context.Response.WriteAsync(errorTxt);
        }
    }
}
