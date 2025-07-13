using MediatR;
using RadioManager.Application.Shows.Dtos;

namespace RadioManager.Application.Shows.Queries.GetByDate
{
    public sealed record GetShowsQuery(
        DateTime Date) : IRequest<IEnumerable<ShowDto>>;

}
