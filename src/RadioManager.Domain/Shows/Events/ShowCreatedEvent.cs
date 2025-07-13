using RadioManager.Domain.Events;
using RadioManager.Domain.Shows.ValueObjects;
using RadioManager.Domain.Types;

namespace RadioManager.Domain.Shows.Events
{
    public sealed record ShowCreatedEvent(
        AggregateId ShowId,
        ShowTitle Title,
        DateTime StartTime,
        Presenter Presenter) : IDomainEvent;

}
