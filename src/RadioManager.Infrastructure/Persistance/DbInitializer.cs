using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RadioManager.Infrastructure.Persistance
{
    internal sealed class DbInitializer(
        IServiceScopeFactory serviceScopeFactory) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ApplyMigrations();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        private async Task ApplyMigrations()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<RadioManagerDbContext>();
        
            await db.Database.MigrateAsync();
        }
    }
}
