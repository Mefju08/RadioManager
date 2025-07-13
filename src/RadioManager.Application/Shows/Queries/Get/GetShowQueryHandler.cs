using ErrorOr;
using MediatR;
using RadioManager.Application.Shows.Dtos;
using RadioManager.Domain.Repositories;
using RadioManager.Domain.Types;

namespace RadioManager.Application.Shows.Queries.Get
{
    internal sealed class GetShowQueryHandler(
        IShowRepository showRepository) : IRequestHandler<GetShowQuery, ErrorOr<ShowDto>>
    {
        public async Task<ErrorOr<ShowDto>> Handle(GetShowQuery request, CancellationToken cancellationToken)
        {
            var showId = AggregateId.Create(request.ShowId);

            var show = await showRepository.GetAsync(showId);
            if (show is null)
                return Error.NotFound();

            return show.ToDto();
        }
    }
}
