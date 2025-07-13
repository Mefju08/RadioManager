using FluentAssertions;
using RadioManager.Application.Shows.Dtos;
using RadioManager.Domain.Shows.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Unit.Tests
{
    public class ShowMapperTests
    {
        [Fact]
        public void ToDto_ShouldCorrectlyMapAllPropertiesFromShow()
        {
            // arrange
            var show = Show.Create(
                title: "Poranna audycja",
                presenter: "Jan Kowalski",
                startTime: new DateTime(2025, 10, 20, 9, 0, 0),
                durationMinutes: 55);

            // act
            var showDto = show.ToDto();

            // assert
            showDto.Should().NotBeNull();
            showDto.Id.Should().Be(show.Id.Value);
            showDto.Title.Should().Be(show.Title.Value);
            showDto.Presenter.Should().Be(show.Presenter.Value);
            showDto.StartTime.Should().Be(show.TimeSlot.StartTime);
            showDto.EndTime.Should().Be(show.TimeSlot.EndTime);
            showDto.DurationMinutes.Should().Be(show.TimeSlot.DurationMinutes);
        }
    }
}
