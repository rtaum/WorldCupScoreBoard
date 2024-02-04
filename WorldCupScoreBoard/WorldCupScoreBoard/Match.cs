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
    }
}
