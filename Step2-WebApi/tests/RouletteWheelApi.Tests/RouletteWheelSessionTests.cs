using FluentAssertions;
using Module.GameModule.Entities;
using Module.GameModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWheelApi.Tests
{
    public class RouletteWheelSessionTests
    {
        [Fact]
        public void RouletteWheelSession_Instance_ShouldBeSingleton()
        {
            // Arrange
            var instance1 = RouletteWheelSession.Instance;
            var instance2 = RouletteWheelSession.Instance;

            // Act & Assert
            instance1.Should().BeSameAs(instance2);
        }

        [Fact]
        public void RouletteWheelSession_Initialization_ShouldHaveCorrectInitialState()
        {
            // Arrange
            var rouletteWheelSession = RouletteWheelSession.Instance;

            // Act & Assert
            rouletteWheelSession.State.Should().Be(SessionState.Initial);
            rouletteWheelSession.StartTime.Should().BeBefore(DateTime.UtcNow);
            rouletteWheelSession.EndTime.Should().BeNull();
            rouletteWheelSession.SelectedNumber.Should().BeNull();
            rouletteWheelSession.PocketColor.Should().BeNull();
            rouletteWheelSession.SessionBets.Should().BeNull();
        }

        [Fact]
        public void RouletteWheelSession_Initialization_ShouldGenerateUniqueId()
        {
            // Arrange
            var rouletteWheelSession = RouletteWheelSession.Instance;

            // Act & Assert
            rouletteWheelSession.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void RouletteWheelSession_Spin_ShouldChangeStateToSpinning()
        {
            // Arrange
            var rouletteWheelSession = RouletteWheelSession.Instance;

            // Act
            rouletteWheelSession.Spin();

            // Assert
            rouletteWheelSession.State.Should().Be(SessionState.Spinning);
        }

        [Fact]
        public void RouletteWheelSession_EndSession_ShouldUpdateStateAndEndTime()
        {
            // Arrange
            var rouletteWheelSession = RouletteWheelSession.Instance;

            // Act
            rouletteWheelSession.Spin(); // Change state to Spinning
            rouletteWheelSession.EndSession();

            // Assert
            rouletteWheelSession.State.Should().Be(SessionState.Initial);
            rouletteWheelSession.EndTime.Should().NotBeNull();
        }

        [Fact]
        public void RouletteWheelSession_EndSession_WithoutSpinning_ShouldThrowException()
        {
            // Arrange
            var rouletteWheelSession = RouletteWheelSession.Instance;

            // Act
            Action act = () => rouletteWheelSession.EndSession();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("State must be a valid state.");
        }

        [Fact]
        public void RouletteWheelSession_GetWinningPocket_ShouldReturnCorrectPocket()
        {
            // Arrange
            var rouletteWheelSession = RouletteWheelSession.Instance;

            // Act
            rouletteWheelSession.Spin(); // Change state to Spinning
            rouletteWheelSession.EndSession(); // End session to set SelectedNumber

            // Assert
            rouletteWheelSession.GetWinningPocket().Number.Should().Be(rouletteWheelSession.SelectedNumber);
        }

        [Fact]
        public void RouletteWheelSession_PlaceBet_ShouldAddBetToSessionBets()
        {
            // Arrange
            var rouletteWheelSession = RouletteWheelSession.Instance;
            var sessionBet = SessionBet.Create(0, BetType.Color, 0, Guid.NewGuid(), "");

            // Act
            rouletteWheelSession.PlaceBet(sessionBet);

            // Assert
            rouletteWheelSession.SessionBets.Should().Contain(sessionBet);
        }

        [Fact]
        public void RouletteWheelSession_UpdateWinnings_ShouldReplaceSessionBets()
        {
            // Arrange
            var rouletteWheelSession = RouletteWheelSession.Instance;
            var newBets = new List<SessionBet> { SessionBet.Create(0,BetType.Color,0,Guid.NewGuid(), "") };

            // Act
            rouletteWheelSession.UpdateWinnings(newBets);

            // Assert
            rouletteWheelSession.SessionBets.Should().BeEquivalentTo(newBets);
        }
    }
}
