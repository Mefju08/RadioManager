using RadioManager.Domain.Shows.Aggregates;

namespace RadioManager.Domain.Shows.Services
{
    public interface IShowSchedulingService
    {
        void EnsureScheduleDoesNotOverlap(Show newShow, IEnumerable<Show> existingShowsInSameDate);
    }
}
