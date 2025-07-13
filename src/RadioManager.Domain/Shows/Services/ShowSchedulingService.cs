using RadioManager.Domain.Shows.Aggregates;
using RadioManager.Domain.Shows.Exceptions;

namespace RadioManager.Domain.Shows.Services
{
    internal sealed class ShowSchedulingService : IShowSchedulingService
    {
        public void EnsureScheduleDoesNotOverlap(Show newShow, IEnumerable<Show> existingShowsInSameDate)
        {
            bool isOverlap = existingShowsInSameDate
                .Select(x => x.TimeSlot)
                .Any(t => t.IsOverlapWith(newShow.TimeSlot));

            if (isOverlap)
                throw new ShowScheduleConflictException(newShow.Title);
        }
    }
}
