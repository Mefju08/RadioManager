using RadioManager.Domain.Shows.Aggregates;
using RadioManager.Domain.Types;

namespace RadioManager.Domain.Repositories
{
    public interface IShowRepository
    {
        Task<Show> GetAsync(AggregateId id);
        Task AddAsync(Show show);
        Task<IList<Show>> GetByDateAsync(DateTime date);
    }
}
