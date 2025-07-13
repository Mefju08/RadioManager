using MediatR;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using RadioManager.Application.Common.Notifications;
using RadioManager.Application.Shows.Commands.Create;
using RadioManager.Integration.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Integration.Tests
{
    public sealed class ShowCreationNotificationTests : IClassFixture<DatabaseFixture>, IAsyncLifetime
    {
        private readonly DatabaseFixture _fixture;
        private readonly Func<Task> _resetDatabase;
        private IServiceScope _scope;

        public ShowCreationNotificationTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _resetDatabase = fixture.ResetDatabaseAsync;
        }

        [Fact]
        public async Task CreateShow_WhenSuccessful_ShouldTriggerNotification()
        {
            // arrange
            var notificationSender = Substitute.For<INotificationSender>();
            var factoryWithMock = _fixture.Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(notificationSender);
                });
            });

            _scope = factoryWithMock.Services.CreateScope();
            var sender = _scope.ServiceProvider.GetRequiredService<ISender>();

            var command = new CreateShowCommand(
                "Wieczorna audycja specjalna",
                "Znany Dziennikarz",
                DateTime.UtcNow.AddDays(7),
                55
            );

            // act
            await sender.Send(command);

            // assert
            await notificationSender.Received(1).SendShowCreatedAsync(
                command.Title,
                Arg.Is<DateTime>(dt => dt.Date == command.StartTime.Date),
                command.Presenter
            );
        }

        public async Task InitializeAsync()
        {
            await _resetDatabase();
        }
        public async Task DisposeAsync()
        {
            _scope.Dispose();
            await Task.CompletedTask;
        }
    }
}
