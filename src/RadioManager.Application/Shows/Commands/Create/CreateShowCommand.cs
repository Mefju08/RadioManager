using MediatR;

namespace RadioManager.Application.Shows.Commands.Create
{
    public sealed record CreateShowCommand(
        string Title,
        string Presenter,
        DateTime StartTime,
        int DurationMinutes) : IRequest<Guid>;
}
