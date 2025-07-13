using MediatR;
using RadioManager.Application.Reports.Dtos;

namespace RadioManager.Application.Reports.Queries.GetDaily
{
    public sealed record GetDailyReportQuery(
        DateTime Date) : IRequest<DailyReportDto>;
}
