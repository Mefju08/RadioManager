using Microsoft.AspNetCore.Http;
using RadioManager.Infrastructure.Exceptions.Generators;
using RadioManager.Infrastructure.Exceptions.Mappers;
using RadioManager.Infrastructure.Logging;
namespace RadioManager.Infrastructure.Exceptions
{
    internal class ExceptionHandlerMiddleware(
        IExceptionToErrorMapper exceptionMapper,
        IEnumerable<IErrorLoggingStrategy> loggingStrategies,
        IErrorResponseGenerator responseGenerator) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception exception)
            {
                var error = exceptionMapper.Map(exception);

                var strategy = loggingStrategies.First(s => s.CanHandle(error));
                if (strategy is not null)
                    strategy.Log(error, context, exception);

                await responseGenerator.GenerateResponseAsync(context, error);
            }
        }
    }
}
