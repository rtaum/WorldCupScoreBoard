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
            Match match = BuildMatch();
            scoreboard.AddMatch(match);

            scoreboard.GetMatchesSummary().Should().HaveCount(1);
        }

        [Fact]
        public void Scoreboard_Should_Not_Allow_Duplicate_Matches()
        {
            var scoreboard = new Scoreboard();
            Match match = BuildMatch();
            scoreboard.AddMatch(match);

            Action act = () => scoreboard.AddMatch(match);

            act.Should().Throw<ArgumentException>()
                .WithMessage("Scoreboard already contains a match with the same teams");
        }

        private static Match BuildMatch()
        {
            var homeTeam = new Team("Germany");
            var awayTeam = new Team("Brazil");
            var startTime = new DateTime(2024, 1, 1);

            var match = new Match(homeTeam, awayTeam, startTime);
            return match;
        }
    }
}
