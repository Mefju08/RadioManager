using FluentAssertions;
using NSubstitute;
using RadioManager.Application.Reports.Queries.GetDaily;
using RadioManager.Domain.Repositories;
using RadioManager.Domain.Shows.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Unit.Tests
{
    public sealed class GetDailyReportQueryHandlerTests
    {
        private readonly IShowRepository _showRepository;
        private readonly GetDailyReportQueryHandler _handler;

        public GetDailyReportQueryHandlerTests()
        {
            _showRepository = Substitute.For<IShowRepository>();
            _handler = new GetDailyReportQueryHandler(_showRepository);
        }

        public static IEnumerable<object[]> ReportTestData()
        {
            yield return new object[] { new List<int> { 60, 30, 15 }, 105 };

            yield return new object[] { new List<int> { 90 }, 90 };

            yield return new object[] { new List<int> { 45, 5, 20 }, 70 };
        }

        [Theory]
        [MemberData(nameof(ReportTestData))]
        public async Task Handle_WithVariousShows_ShouldCalculateTotalDurationCorrectly(
            List<int> durations, int expectedTotalDuration)
        {
            // arrange
            var queryDate = new DateTime(2025, 7, 15);
            var query = new GetDailyReportQuery(queryDate);

            var shows = durations.Select(duration =>
                Show.Create("Test Show", "Test Presenter", queryDate, duration)).ToList();

            _showRepository.GetByDateAsync(query.Date).Returns(shows);

            // act
            var report = await _handler.Handle(query, CancellationToken.None);

            // assert
            report.Should().NotBeNull();
            report.TotalDurationTime.Should().Be(expectedTotalDuration);            
        }
    }
}
