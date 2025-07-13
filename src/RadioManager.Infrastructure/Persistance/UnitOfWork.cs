using MediatR;
using RadioManager.Application.Common;
using RadioManager.Domain.Types;

namespace RadioManager.Infrastructure.Persistance
{
    internal class UnitOfWork(RadioManagerDbContext db, IPublisher publisher) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var eggegatesWithEvents = db.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Count > 0)
                .ToList();

            var result = await db.SaveChangesAsync(cancellationToken);
            foreach (var aggregate in eggegatesWithEvents)
            {
                var events = aggregate.DomainEvents.ToArray();
                aggregate.ClearDomainEvents();

                foreach (var @event in events)
                {
                    await publisher.Publish(@event, cancellationToken);
                }
            }

            return result;
        }
    }
}
