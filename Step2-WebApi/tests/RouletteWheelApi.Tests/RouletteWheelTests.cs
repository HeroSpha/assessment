using FluentAssertions;
using Module.GameModule.Enums;

namespace RouletteWheelApi.Tests
{
    public class RouletteWheelTests
    {

        [Fact]
        public void RouletteWheel_Instance_ShouldBeSingleton()
        {
            // Arrange
            var instance1 = Module.GameModule.Entities.RouletteWheel.Instance;
            var instance2 = Module.GameModule.Entities.RouletteWheel.Instance;

            // Act & Assert
            instance1.Should().BeSameAs(instance2);
        }

        [Fact]
        public void RouletteWheel_Pockets_ShouldHaveCorrectCount()
        {
            // Arrange
            var rouletteWheel = Module.GameModule.Entities.RouletteWheel.Instance;

            // Act & Assert
            rouletteWheel.Pockets.Should().HaveCount(37);
        }

        [Fact]
        public void RouletteWheel_Pockets_ShouldContainGreenPocket()
        {
            // Arrange
            var rouletteWheel = Module.GameModule.Entities.RouletteWheel.Instance;

            // Act & Assert
            rouletteWheel.Pockets.Should().ContainSingle(pocket => pocket.Color == PocketColor.Green);
        }

        [Theory]
        [InlineData(0, PocketColor.Green)]
        [InlineData(1, PocketColor.Black)]
        [InlineData(2, PocketColor.Red)]
        [InlineData(3, PocketColor.Black)]
        public void RouletteWheel_Pockets_ShouldHaveCorrectColors(int pocketNumber, PocketColor expectedColor)
        {
            // Arrange
            var rouletteWheel = Module.GameModule.Entities.RouletteWheel.Instance;

            // Act & Assert
            rouletteWheel.Pockets.Should().ContainSingle(pocket => pocket.Number == pocketNumber && pocket.Color == expectedColor);
        }
    }
}