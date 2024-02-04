namespace WorldCupScoreBoard
{
    public class Match
    {
        public Match(Team homeTeam, Team awayTeam)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }

        public Team HomeTeam { get; }
        
        public Team AwayTeam { get; }
    }
}
