using RadioManager.Domain.Shows.Aggregates;

namespace RadioManager.Application.Shows.Dtos
{
    internal sealed record ShowDto(
        Guid Id,
        string Title,
        string Presenter,
        DateTime StartTime,
        DateTime EndTime,
        int DurationMinutes);


    internal static class ShowMapper
    {
        public static ShowDto ToDto(this Show show)
            => new ShowDto(
                 show.Id,
                 show.Title,
                 show.Presenter,
                 show.TimeSlot.StartTime,
                 show.TimeSlot.EndTime,
                 show.TimeSlot.DurationMinutes);
    }
}
