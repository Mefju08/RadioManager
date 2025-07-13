using FluentAssertions;
using MediatR;
using RadioManager.Domain.Shows.Aggregates;
using RadioManager.Domain.Shows.Exceptions;
using RadioManager.Domain.Shows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Unit.Tests
{
    public class ShowSchedulingServiceTests
    {
        private readonly IShowSchedulingService _schedulingService;

        public ShowSchedulingServiceTests()
        {
            _schedulingService = new ShowSchedulingService();
        }

        [Fact]
        public void EnsureScheduleDoesNotOverlap_WhenShowsValid_ShouldNotThrowException()
        {
            // arrange
            var newShow = Show.Create("Audycja 10:00", "Prezenter B", new DateTime(2025, 1, 1, 10, 0, 0), 60);
            var existingShows = new List<Show>
            {
                Show.Create("Audycja 9:00", "Prezenter A", new DateTime(2025, 1, 1, 9, 0, 0), 60)
            };

            // act
            var result = () => _schedulingService.EnsureScheduleDoesNotOverlap(newShow, existingShows);

            // assert
            result.Should().NotThrow();
        }

        public static IEnumerable<object[]> GetOverlappingScenarios()
        {
            yield return new object[] { Show.Create("Konflikt 1", "P. Nowak", new DateTime(2025, 1, 1, 9, 30, 0), 30) };
            yield return new object[] { Show.Create("Konflikt 2", "A. Kowalska", new DateTime(2025, 1, 1, 8, 30, 0), 60) };
            yield return new object[] { Show.Create("Konflikt 3", "J. Wiśniewski", new DateTime(2025, 1, 1, 8, 0, 0), 180) };
            yield return new object[] { Show.Create("Konflikt 4", "K. Wójcik", new DateTime(2025, 1, 1, 9, 15, 0), 30) };
            yield return new object[] { Show.Create("Konflikt 5", "M. Zając", new DateTime(2025, 1, 1, 9, 0, 0), 60) };
        }

        [Theory]
        [MemberData(nameof(GetOverlappingScenarios))]
        public void EnsureScheduleDoesNotOverlap_WhenOverlapExists_ShouldThrowShowScheduleConflictException(Show conflictingShow)
        {   // arrange
            var existingShow = Show.Create("Istniejąca audycja", "Główny Prezenter", new DateTime(2025, 1, 1, 9, 0, 0), 60);
            var existingShows = new List<Show> { existingShow };

            // act
            var result = () => _schedulingService.EnsureScheduleDoesNotOverlap(conflictingShow, existingShows);

            // assert
            result.Should().Throw<ShowScheduleConflictException>();
                
        }
    }
}
