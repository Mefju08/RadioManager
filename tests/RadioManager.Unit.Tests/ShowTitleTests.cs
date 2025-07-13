using FluentAssertions;
using RadioManager.Domain.Shows.Exceptions;
using RadioManager.Domain.Shows.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Unit.Tests
{
    public class ShowTitleTests
    {
        [Fact]
        public void Create_WithValidTitle_ShouldSetValueCorrectly()
        {
            // arrange
            var validTitle = "Poranna audycja";

            // act
            var showTitle = ShowTitle.Create(validTitle);

            // assert
            showTitle.Should().NotBeNull();
            showTitle.Value.Should().Be(validTitle);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WithInvalidTitle_ShouldThrowEmptyShowTitleException(string invalidTitle)
        {
            // act
            var act = () => ShowTitle.Create(invalidTitle);

            // assert 
            act.Should().Throw<EmptyShowTitleException>();
        }

        [Fact]
        public void ImplicitConversion_ToString_ShouldReturnCorrectValue()
        {
            // arrange
            var expectedTitle = "Wiadomości";

            // act
            var showTitle = ShowTitle.Create(expectedTitle);
            string result = showTitle;

            // assert
            result.Should().Be(expectedTitle);
        }
    }
}
