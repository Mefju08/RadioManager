using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RadioManager.Api;
using RadioManager.Application.Shows.Commands.Create;
using RadioManager.Infrastructure.Persistance;
using RadioManager.Integration.Tests.Fixtures;
using System.Net.Http.Json;
namespace RadioManager.Integration.Tests
{
    public sealed class ShowControllerTest : IClassFixture<DatabaseFixture>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly Func<Task> _resetDatabase;
        public ShowControllerTest(DatabaseFixture fixture)
        {
            _factory = fixture.Factory;
            _client = _factory.CreateClient();
            _resetDatabase = fixture.ResetDatabaseAsync;
        }

        [Fact]
        public async Task Create_WithValidShowPayload_ShouldCreateShow()
        {
            // arrange
            var showPayload = new CreateShowCommand(
                "test title", "test presenter", DateTime.UtcNow.AddDays(1), 20);

            // act
            var response = await _client.PostAsJsonAsync("/api/shows", showPayload);

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<RadioManagerDbContext>();
            var showCount = dbContext.Shows.Count();
            showCount.Should().Be(1);
        }

        [Fact]
        public async Task Create_WithConflictingShowPayload_ShouldReturn400BadRequest()
        {
            // arrange
            var initialShowPayload = new CreateShowCommand(
             "Initial Show", "First Presenter", DateTime.UtcNow.AddDays(1), 30);
            await _client.PostAsJsonAsync("/api/shows", initialShowPayload);

            var conflictingShowPayload = new CreateShowCommand(
            "Conflicting Show", "Second Presenter", DateTime.UtcNow.AddDays(1), 20);

            // act
            var response = await _client.PostAsJsonAsync("/api/shows", conflictingShowPayload);

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<RadioManagerDbContext>();
            var showCount = dbContext.Shows.Count();
            showCount.Should().Be(1);
        }

        public async Task InitializeAsync()
        {
            await _resetDatabase();
        }
        
        public async Task DisposeAsync()
        {
            await Task.CompletedTask;
        }
    }
}
