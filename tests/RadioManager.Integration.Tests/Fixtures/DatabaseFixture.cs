using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RadioManager.Api;
using RadioManager.Infrastructure.Persistance;
using Respawn;
using Testcontainers.MsSql;

namespace RadioManager.Integration.Tests.Fixtures
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer;
        public WebApplicationFactory<Program> Factory { get; private set; }
        private Respawner _respawner;
        public DatabaseFixture()
        {
            _dbContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("yourStrong(!)Password")
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            Factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<RadioManagerDbContext>));
                        if (descriptor != null) services.Remove(descriptor);

                        services.AddDbContext<RadioManagerDbContext>(options =>
                            options.UseSqlServer(_dbContainer.GetConnectionString()));
                    });
                });

            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<RadioManagerDbContext>();
            await dbContext.Database.MigrateAsync();

            _respawner = await Respawner.CreateAsync(_dbContainer.GetConnectionString(), new RespawnerOptions
            {
                TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
            });
        }
        public Task ResetDatabaseAsync() => _respawner.ResetAsync(_dbContainer.GetConnectionString());
        public async Task DisposeAsync()
        {
            await Factory.DisposeAsync();
            await _dbContainer.StopAsync();
        }
    }
}
