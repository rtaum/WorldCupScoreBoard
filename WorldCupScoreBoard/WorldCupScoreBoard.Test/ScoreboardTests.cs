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

        [Fact]
        public void Scoreboard_Should_Contain_Matches_When_Added()
        {
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Germany");
            var awayTeam = new Team("Brazil");
            var startTime = new DateTime(2024, 1, 1);

            var match = new Match(homeTeam, awayTeam, startTime);
            scoreboard.AddMatch(match);

            scoreboard.GetMatchesSummary().Should().HaveCount(1);
        }
    }
}
