using FluentAssertions;

namespace WorldCupScoreBoard.Test
{
    public class MatchTests
    {
        private Match _match;
        private Team _homeTeam;
        private Team _awayTeam;

        public MatchTests()
        {
            _homeTeam = new Team("Germany");
            _awayTeam = new Team("Brazil");
            _match = new Match(_homeTeam, _awayTeam);
        }

        [Fact]
        public void Match_Should_Contain_Correct_Teams()
        {
            _match.HomeTeam.Should().Be(_homeTeam);
            _match.AwayTeam.Should().Be(_awayTeam);
        }
    }
}