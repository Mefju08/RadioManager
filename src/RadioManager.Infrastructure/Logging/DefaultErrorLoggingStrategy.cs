using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RadioManager.Infrastructure.Exceptions;
using RadioManager.Infrastructure.Exceptions.Models;

namespace RadioManager.Infrastructure.Logging
{
    internal class DefaultErrorLoggingStrategy(ILogger<ExceptionHandlerMiddleware> logger) : IErrorLoggingStrategy
    {
        public bool CanHandle(Error error) => error.HttpCode != System.Net.HttpStatusCode.BadRequest;

        public void Log(Error error, HttpContext context, Exception originalException)
        {
            logger.LogError(originalException, "Unhandled exception for {Method} {Path}",
                context.Request.Method, context.Request.Path);
        }
    }
}
