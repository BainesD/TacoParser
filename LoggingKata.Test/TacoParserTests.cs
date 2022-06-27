using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldDoSomething()
        {

            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");

            //Assert
            Assert.NotNull(actual);

        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        [InlineData("32.072974,-84.222921,Taco Bell Americu...", -84.222921)]
        public void ShouldParseLongitude(string line, double expected)
        {
            //Arrange
            var tacoParser2 = new TacoParser();

            //Act
            var actual = tacoParser2.Parse(line).Location.Longitude;

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData("34.992219, -86.841402, Taco Bell Ardmore...", 34.992219)]
        [InlineData("32.571331,-85.499655,Taco Bell Auburn...", 32.571331)]
        public void ShouldParseLatitude(string line, double expected)
        {
            //Arrange
            var tacoParser3 = new TacoParser();

            //Act
            var actual = tacoParser3.Parse(line).Location.Latitude;

            //Assert
            Assert.Equal(actual, expected);
        }

    }
}
