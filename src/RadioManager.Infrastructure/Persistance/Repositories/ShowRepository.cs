using Microsoft.EntityFrameworkCore;
using RadioManager.Domain.Repositories;
using RadioManager.Domain.Shows.Aggregates;
using RadioManager.Domain.Types;

namespace RadioManager.Infrastructure.Persistance.Repositories
{
    internal sealed class ShowRepository(
        RadioManagerDbContext db) : IShowRepository
    {
        public Task AddAsync(Show show)
            => Task.FromResult(db.Shows.AddAsync(show));

        public Task<Show> GetAsync(AggregateId id)
            => db.Shows.SingleOrDefaultAsync(show => show.Id == id);


        public async Task<IList<Show>> GetByDateAsync(DateTime date)
            => await db.Shows
                .Where(x => x.TimeSlot.StartTime.Date == date.Date)
                .ToListAsync();
    }
}
