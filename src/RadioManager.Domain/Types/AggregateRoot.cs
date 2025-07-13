using RadioManager.Domain.Events;

namespace RadioManager.Domain.Types
{
    public abstract class AggregateRoot<T>
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        public T Id { get; protected set; }
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public void AddEvent(IDomainEvent domainEvent)
            => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents()
            => _domainEvents.Clear();
    }

    public abstract class AggregateRoot : AggregateRoot<AggregateId>
    {
    }
}
