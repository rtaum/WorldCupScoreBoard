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

        [Fact]
        public void Match_Should_Start_With_No_Scores()
        {
            _match.HomeTeamScore.Should().Be(0);
            _match.AwayTeamScore.Should().Be(0);
        }

        [Fact]
        public void Match_Home_Team_Goal_Changes_Score_Value()
        {
            _match.TeamScoredGoal(_homeTeam);
            _match.HomeTeamScore.Should().Be(1);
            _match.AwayTeamScore.Should().Be(0);
        }

        [Fact]
        public void Match_Away_Team_Goal_Changes_Score_Value()
        {
            _match.TeamScoredGoal(_awayTeam);
            _match.HomeTeamScore.Should().Be(0);
            _match.AwayTeamScore.Should().Be(1);
        }
    }
}