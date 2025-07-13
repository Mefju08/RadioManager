using RadioManager.Infrastructure.Exceptions.Models;

namespace RadioManager.Infrastructure.Exceptions.Mappers
{
    internal interface IExceptionToErrorMapper
    {
        Error Map(Exception exception);
    }
}