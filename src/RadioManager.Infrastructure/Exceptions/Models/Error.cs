using System.Net;

namespace RadioManager.Infrastructure.Exceptions.Models
{
    internal sealed record Error(
        string Code,
        string Message,
        HttpStatusCode HttpCode);
}
