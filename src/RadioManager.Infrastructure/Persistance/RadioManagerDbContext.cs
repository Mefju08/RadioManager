using Microsoft.EntityFrameworkCore;
using RadioManager.Domain.Shows.Aggregates;

namespace RadioManager.Infrastructure.Persistance
{
    sealed class RadioManagerDbContext : DbContext
    {
        public DbSet<Show> Shows { get; set; }
        public RadioManagerDbContext(DbContextOptions<RadioManagerDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.HasDefaultSchema("shows");
        }
    }
}
