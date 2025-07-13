using MediatR;
using RadioManager.Application.Shows.Dtos;
using RadioManager.Domain.Repositories;

namespace RadioManager.Application.Shows.Queries.GetByDate
{
    internal sealed class GetShowsQueryHandler(
        IShowRepository showRepository) : IRequestHandler<GetShowsQuery, IEnumerable<ShowDto>>
    {
        public async Task<IEnumerable<ShowDto>> Handle(GetShowsQuery request, CancellationToken cancellationToken)
        {
            var shows = await showRepository.GetByDateAsync(request.Date);
            if (!shows.Any())
                return Enumerable.Empty<ShowDto>();

            return shows.Select(x => x.ToDto());
        }
    }
}
