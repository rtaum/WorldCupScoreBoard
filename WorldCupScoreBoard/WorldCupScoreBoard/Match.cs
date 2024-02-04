namespace WorldCupScoreBoard
{
    public class Match
    {
        public Match(Team homeTeam, Team awayTeam, DateTime startTime)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            StartTime = startTime;
            HomeTeamScore = 0;
            AwayTeamScore = 0;
            Status = MatchStatus.None;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public Team HomeTeam { get; }

        public Team AwayTeam { get; }

        public DateTime StartTime { get; }

        public int HomeTeamScore { get; private set; }

        public int AwayTeamScore { get; private set; }

        public MatchStatus Status { get; private set; }

        public string Summary => $"{HomeTeam.Name} {HomeTeamScore} - {AwayTeam.Name} {AwayTeamScore}";

        public void Start()
        {
            if (Status == MatchStatus.Started)
            {
                throw new InvalidOperationException("Match is already started");
            }

            if (Status == MatchStatus.Finished)
            {
                throw new InvalidOperationException("Match is already finished");
            }

            Status = MatchStatus.Started;
        }

        public void Finish()
        {
            Status = MatchStatus.Finished;
        }

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

        public void UpdateScores(int homeTeamScore, int awayTeamScore)
        {
            if (homeTeamScore < 0 || awayTeamScore < 0)
            {
                // Scores cannot have negative values
                throw new ArgumentException("Score can not be negative");
            }

            // theoretically score can change in any way. Goals can be scored or cancelled.
            HomeTeamScore = homeTeamScore;
            AwayTeamScore = awayTeamScore;
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
