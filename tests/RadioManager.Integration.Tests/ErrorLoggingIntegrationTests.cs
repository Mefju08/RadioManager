using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using RadioManager.Application.Shows.Commands.Create;
using RadioManager.Infrastructure.Exceptions;
using RadioManager.Integration.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Integration.Tests
{
    public sealed class ErrorLoggingIntegrationTests : IClassFixture<DatabaseFixture>, IAsyncLifetime
    {
        private readonly DatabaseFixture _fixture;
        private readonly Func<Task> _resetDatabase;

        public ErrorLoggingIntegrationTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _resetDatabase = fixture.ResetDatabaseAsync;
        }

        [Fact]
        public async Task Create_WithConflictingShowPayload_ShouldLogBusinessError()
        {
            // arrange
            var loggerMock = Substitute.For<ILogger<BusinessErrorCategory>>(); 
            var factoryWithMock = _fixture.Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(loggerMock);
                });
            });

            var client = factoryWithMock.CreateClient();

            var initialShowPayload = new CreateShowCommand(
                "Audycja Pierwotna", "Prezenter 1", DateTime.UtcNow.AddDays(1), 30);
            await client.PostAsJsonAsync("/api/shows", initialShowPayload);

            var conflictingShowPayload = new CreateShowCommand(
                "Audycja Konfliktowa", "Prezenter 2", DateTime.UtcNow.AddDays(1), 20);

            // act
            await client.PostAsJsonAsync("/api/shows", conflictingShowPayload);

            // assert
            loggerMock.Received(1).Log(
                LogLevel.Error,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString().Contains("conflicts with existing schedule")),
                null, 
                Arg.Any<Func<object, Exception, string>>());
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
