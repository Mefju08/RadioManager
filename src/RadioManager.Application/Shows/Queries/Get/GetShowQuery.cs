using ErrorOr;
using MediatR;
using RadioManager.Application.Shows.Dtos;

namespace RadioManager.Application.Shows.Queries.Get
{
    public sealed record GetShowQuery(
        Guid ShowId) : IRequest<ErrorOr<ShowDto>>;
}
