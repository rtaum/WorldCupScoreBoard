using FluentAssertions;

namespace WorldCupScoreBoard.Test
{
    public class ScoreboardTests
    {
        [Fact]
        public void Scoreboard_Should_Be_Empty()
        {
            var scoreboard = new Scoreboard();

            scoreboard.GetMatchesSummary().Should().BeEmpty();
        }
    }
}
