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
            var awaitTeam = new Team("France");

            var newMatchId = scoreboard.StartMatch(homeTeam, awaitTeam);

            var summary = scoreboard.GetMatchesSummary();
            summary.Should().HaveCount(1);
            summary.Should().Contain((m) => m.Id == newMatchId);
        }

        [Fact]
        public void Scoreboard_Should_Not_Allow_Duplicate_Matches()
        {
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Germany");
            var awaitTeam = new Team("France");

            var newMatchId = scoreboard.StartMatch(homeTeam, awaitTeam);

            Action act = () => scoreboard.StartMatch(homeTeam, awaitTeam);

            act.Should().Throw<ArgumentException>()
                .WithMessage("Scoreboard already contains a match with the same teams");
        }

        [Fact]
        public void Scoreboard_Should_Contain_Only_Started_Matches()
        {
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Germany");
            var awaitTeam = new Team("France");
            var newMatchId = scoreboard.StartMatch(homeTeam, awaitTeam);

            var homeTeam2 = new Team("Italy");
            var awaitTeam2 = new Team("Spain");
            var newMatchId2 = scoreboard.StartMatch(homeTeam2, awaitTeam2);

            var summary = scoreboard.GetMatchesSummary();
            summary.Should().NotContain((m) => m.Status != MatchStatus.Started);
        }

        [Fact]
        public void Scoreboard_Should_Allow_To_Finish_Started_Match()
        {
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Germany");
            var awaitTeam = new Team("France");

            var newMatchId = scoreboard.StartMatch(homeTeam, awaitTeam);
            scoreboard.FinishMatch(newMatchId);

            var summary = scoreboard.GetMatchesSummary();
            summary.Should().BeEmpty();
        }

        [Fact]
        public void Scoreboard_Finish_Match_Should_Throw_Exception_If_Match_Cannot_Be_Found()
        {
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Germany");
            var awaitTeam = new Team("France");

            scoreboard.StartMatch(homeTeam, awaitTeam);

            var matchId = Guid.Empty;
            Action act = () => scoreboard.FinishMatch(matchId);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"Match with Id '{matchId}' cannot be found");
        }

        [Fact]
        public void Scoreboard_Should_Allow_To_Update_Score()
        {
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Germany");
            var awaitTeam = new Team("France");

            var matchId = scoreboard.StartMatch(homeTeam, awaitTeam);
            scoreboard.UpdateMatchScore(matchId, 2, 1);

            var summary = scoreboard.GetMatchesSummary();
            summary.Should().HaveCount(1);
            summary.First().Summary.Should().Be("Germany 2 - France 1");
        }

        [Fact]
        public void Scoreboard_Update_Match_Score_Should_Throw_Exception_If_Match_Cannot_Be_Found()
        {
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Germany");
            var awaitTeam = new Team("France");

            scoreboard.StartMatch(homeTeam, awaitTeam);

            var matchId = Guid.Empty;
            Action act = () => scoreboard.UpdateMatchScore(matchId, 0, 0);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"Match with Id '{matchId}' cannot be found");
        }

        [Fact]
        public void Scoreboard_Finish_Match_Removes_Match_From_Scoreboard()
        {
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Germany");
            var awaitTeam = new Team("France");
            var newMatchId = scoreboard.StartMatch(homeTeam, awaitTeam);

            var homeTeam2 = new Team("Italy");
            var awaitTeam2 = new Team("Spain");
            var newMatchId2 = scoreboard.StartMatch(homeTeam2, awaitTeam2);

            scoreboard.FinishMatch(newMatchId2);

            var summary = scoreboard.GetMatchesSummary();
            summary.Should().HaveCount(1);
            summary.First().Id.Should().Be(newMatchId);
        }

        [Fact]
        public void Scoreboard_Match_With_Most_Total_Score_Should_Come_First()
        {
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Germany");
            var awaitTeam = new Team("France");
            var newMatchId = scoreboard.StartMatch(homeTeam, awaitTeam);

            var homeTeam2 = new Team("Italy");
            var awaitTeam2 = new Team("Spain");
            var newMatchId2 = scoreboard.StartMatch(homeTeam2, awaitTeam2);

            scoreboard.UpdateMatchScore(newMatchId, 1, 0);

            var summary = scoreboard.GetMatchesSummary();
            summary.Should().HaveCount(2);
            summary.First().Id.Should().Be(newMatchId);
            summary.Last().Id.Should().Be(newMatchId2);
        }
    }
}
