using MediatR;
using RadioManager.Application.Reports.Dtos;
using RadioManager.Application.Shows.Dtos;
using RadioManager.Domain.Repositories;

namespace RadioManager.Application.Reports.Queries.GetDaily
{
    internal sealed class GetDailyReportQueryHandler(
        IShowRepository showRepository) : IRequestHandler<GetDailyReportQuery, DailyReportDto>
    {
        public async Task<DailyReportDto> Handle(GetDailyReportQuery request, CancellationToken cancellationToken)
        {
            var shows = await showRepository.GetByDateAsync(request.Date);
            var showsList = shows.ToList();

            return new DailyReportDto(
                request.Date,
                showsList.Count,
                showsList.Sum(x => x.TimeSlot.DurationMinutes),
                showsList.Select(x => x.ToDto()));
        }
    }
}
