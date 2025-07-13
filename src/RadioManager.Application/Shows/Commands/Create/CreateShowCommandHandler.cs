using MediatR;
using RadioManager.Application.Common;
using RadioManager.Domain.Repositories;
using RadioManager.Domain.Shows.Aggregates;
using RadioManager.Domain.Shows.Services;

namespace RadioManager.Application.Shows.Commands.Create
{
    internal sealed class CreateShowCommandHandler(
        IShowRepository showRepository,
        IShowSchedulingService showSchedulingService,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateShowCommand, Guid>
    {
        public async Task<Guid> Handle(CreateShowCommand request, CancellationToken cancellationToken)
        {
            var show = Show.Create(request.Title, request.Presenter, request.StartTime,
                request.DurationMinutes);

            var showsInSameDate = await showRepository.GetByDateAsync(show.TimeSlot.StartTime);
            showSchedulingService.EnsureScheduleDoesNotOverlap(show, showsInSameDate);

            await showRepository.AddAsync(show);
            await unitOfWork.SaveChangesAsync();

            return show.Id;
        }
    }
}
