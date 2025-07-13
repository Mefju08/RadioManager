using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RadioManager.Infrastructure.Exceptions;
using RadioManager.Infrastructure.Exceptions.Models;
using System.Net;

namespace RadioManager.Infrastructure.Logging
{
    internal class BusinessErrorLoggingStrategy(ILogger<BusinessErrorCategory> logger) : IErrorLoggingStrategy
    {
        public bool CanHandle(Error error) => error.HttpCode == HttpStatusCode.BadRequest;

        public void Log(Error error, HttpContext context, Exception originalException)
        {
            logger.LogError("{ErrorMessage}", error.Message);
        }
    }
}
