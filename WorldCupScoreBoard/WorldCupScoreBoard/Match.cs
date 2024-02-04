
namespace WorldCupScoreBoard
{
    public class Match
    {
        public Match(Team homeTeam, Team awayTeam)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;

            HomeTeamScore = 0;
            AwayTeamScore = 0;
        }

        public Team HomeTeam { get; }

        public Team AwayTeam { get; }

        public int HomeTeamScore { get; private set; }

        public int AwayTeamScore { get; private set; }

        public void TeamGoalCancelled(Team team)
        {
            if (team == HomeTeam)
            {
                CheckScoreBeforeCancellation(HomeTeamScore);
                HomeTeamScore--;
            }
            else if (team == AwayTeam)
            {
                CheckScoreBeforeCancellation(AwayTeamScore);
                AwayTeamScore--;
            }
            else
            {
                throw new ArgumentException(nameof(team), $"Team {team.Name} is not in the match.");
            }
        }

        public void TeamScored(Team team)
        {
            if (team == HomeTeam)
            {
                HomeTeamScore++;
            }
            else if (team == AwayTeam)
            {
                AwayTeamScore++;
            }
            else
            {
                throw new ArgumentException(nameof(team), $"Team {team.Name} is not in the match.");
            }
        }

        private void CheckScoreBeforeCancellation(int teamScore)
        {
            if (teamScore == 0)
            {
                throw new InvalidOperationException("Score can not be negative");
            }
        }
    }
}
