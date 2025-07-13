using Microsoft.AspNetCore.Http;
using RadioManager.Infrastructure.Exceptions.Models;

namespace RadioManager.Infrastructure.Logging
{
    internal interface IErrorLoggingStrategy
    {
        bool CanHandle(Error error);
        void Log(Error error, HttpContext context, Exception originalException);
    }
}
