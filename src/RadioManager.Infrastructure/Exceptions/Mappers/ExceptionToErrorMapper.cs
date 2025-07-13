using Humanizer;
using RadioManager.Domain.Exceptions;
using RadioManager.Infrastructure.Exceptions.Models;
using System.Net;
namespace RadioManager.Infrastructure.Exceptions.Mappers
{
    internal sealed class ExceptionToErrorMapper : IExceptionToErrorMapper
    {
        public Error Map(Exception exception)
        {
            return exception switch
            {
                RadioManagerException ex => new Error(GetErrorCode(ex), exception.Message, GetHttpCode(ex)),
                _ => new Error("fatal_error", "Unexpected error", HttpStatusCode.InternalServerError),
            };
        }

        private static string GetErrorCode(object obj)
            => obj.GetType().Name.Underscore().ToLower().Replace("_exception", string.Empty);
        private static HttpStatusCode GetHttpCode(object obj)
        {
            string objName = obj.GetType().Name;
            if (objName.Contains("NotFound"))
                return HttpStatusCode.NotFound;

            return HttpStatusCode.BadRequest;
        }
    }
}
