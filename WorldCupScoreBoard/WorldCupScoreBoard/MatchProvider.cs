namespace WorldCupScoreBoard
{
    public class MatchProvider : IMatchProvider
    {
        private readonly List<Match> _matches;

        public MatchProvider()
        {
            _matches = new List<Match>();
        }

        public IReadOnlyCollection<Match> Matches
        {
            get
            {
                return _matches;
            }
        }

        public Match AddNewMatchForTeams(Team homeTeam, Team awayTeam)
        {
            var match = new Match(homeTeam, awayTeam, DateTime.Now);
            _matches.Add(match);

            return match;
        }

        public void RemoveMatch(Match match)
        {
            _matches.Remove(match);
        }

        public Match? GetMatchById(Guid id)
        {
            return _matches.FirstOrDefault(m => m.Id == id);
        }
    }
}
