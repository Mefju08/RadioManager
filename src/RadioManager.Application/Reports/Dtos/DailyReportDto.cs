using RadioManager.Application.Shows.Dtos;

namespace RadioManager.Application.Reports.Dtos
{
    internal sealed record DailyReportDto(
        DateTime Date,
        int TotalShows,
        int TotalDurationTime,
        IEnumerable<ShowDto> Shows);
}
