using FluentAssertions;

namespace WorldCupScoreBoard.Test
{
    public class MatchTests
    {
        private Match _match;
        private Team _homeTeam;
        private Team _awayTeam;
        private Team _outOfRangeTeam;

        public MatchTests()
        {
            _homeTeam = new Team("Germany");
            _awayTeam = new Team("Brazil");
            _outOfRangeTeam = new Team("Italy");
            _match = new Match(_homeTeam, _awayTeam, DateTime.MinValue);
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
            _match.TeamScored(_homeTeam);
            _match.HomeTeamScore.Should().Be(1);
            _match.AwayTeamScore.Should().Be(0);
        }

        [Fact]
        public void Match_Away_Team_Goal_Changes_Score_Value()
        {
            _match.TeamScored(_awayTeam);
            _match.HomeTeamScore.Should().Be(0);
            _match.AwayTeamScore.Should().Be(1);
        }

        [Fact]
        public void Match_Wrong_Team_Goal_Should_Throw_Exception()
        {
            Action act = () => _match.TeamScored(_outOfRangeTeam);
            act.Should().Throw<ArgumentException>()
                .WithParameterName($"Team {_outOfRangeTeam.Name} is not in the match.");
        }

        [Fact]
        public void Match_Home_Team_Goal_Cancelled_Changes_Score_Value()
        {
            _match.TeamScored(_homeTeam);
            _match.TeamScored(_homeTeam);
            _match.TeamScored(_homeTeam);
            _match.TeamGoalCancelled(_homeTeam);

            _match.HomeTeamScore.Should().Be(2);
            _match.AwayTeamScore.Should().Be(0);
        }

        [Fact]
        public void Match_Away_Team_Goal_Cancelled_Changes_Score_Value()
        {
            _match.TeamScored(_awayTeam);
            _match.TeamScored(_awayTeam);
            _match.TeamScored(_awayTeam);
            _match.TeamGoalCancelled(_awayTeam);

            _match.HomeTeamScore.Should().Be(0);
            _match.AwayTeamScore.Should().Be(2);
        }


        [Fact]
        public void Match_Wrong_Team_Goal_Cancelled_Should_Throw_Exception()
        {
            Action act = () => _match.TeamGoalCancelled(_outOfRangeTeam);
            act.Should().Throw<ArgumentException>()
                .WithParameterName($"Team {_outOfRangeTeam.Name} is not in the match.");
        }

        [Fact]
        public void Match_Home_Team_Goal_Cancelled_When_Team_Did_Not_Score_Should_Throw_Exception()
        {
            Action act = () => _match.TeamGoalCancelled(_homeTeam);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Score can not be negative");
        }

        [Fact]
        public void Match_Away_Team_Goal_Cancelled_When_Team_Did_Not_Score_Should_Throw_Exception()
        {
            Action act = () => _match.TeamGoalCancelled(_awayTeam);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Score can not be negative");
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(100, 50)]
        public void Match_Update_Teams_Score_Should_Change_Scores(int homeTeamScore, int awayTeamScore)
        {
            _match.UpdateScores(homeTeamScore, awayTeamScore);

            _match.HomeTeamScore.Should().Be(homeTeamScore);
            _match.AwayTeamScore.Should().Be(awayTeamScore);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        [InlineData(-1, 0)]
        public void Match_Update_Teams_Score_With_Negative_Values_Should_Throw_Exception(int homeTeamScore, int awayTeamScore)
        {
            Action act = () => _match.UpdateScores(homeTeamScore, awayTeamScore);
            act.Should().Throw<ArgumentException>()
                .WithMessage("Score can not be negative");
        }

        [Fact]
        public void Match_StartTime_Should_Be_Correct()
        {
            DateTime startTime = new DateTime(2024, 2, 4);
            var match = new Match(_homeTeam, _awayTeam, startTime);

            match.StartTime.Should().Be(startTime);
        }

        [Fact]
        public void Match_Default_Status_Should_Be_None()
        {
            var match = new Match(_homeTeam, _awayTeam, DateTime.MinValue);

            match.Status.Should().Be(MatchStatus.None);
        }

        [Fact]
        public void Started_Match_Status_Should_Be_Started()
        {
            var match = new Match(_homeTeam, _awayTeam, DateTime.MinValue);
            match.Start();

            match.Status.Should().Be(MatchStatus.Started);
        }

        [Fact]
        public void Finished_Match_Status_Should_Be_Finished()
        {
            var match = new Match(_homeTeam, _awayTeam, DateTime.MinValue);
            match.Finish();

            match.Status.Should().Be(MatchStatus.Finished);
        }
    }
}