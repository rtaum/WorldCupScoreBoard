using System.Collections.Immutable;

namespace WorldCupScoreBoard
{
    public class Scoreboard
    {
        private readonly List<Match> _matches;

        public Scoreboard()
        {
            _matches = new List<Match>();
        }

        public void AddMatch(Match match)
        {
            if (_matches.Any(m => m.HomeTeam == match.HomeTeam
                || m.AwayTeam == match.HomeTeam
                || m.AwayTeam == match.HomeTeam
                || m.AwayTeam == match.AwayTeam))
            {
                throw new ArgumentException("Scoreboard already contains a match with the same teams");
            }

            match.Start();
            _matches.Add(match);
        }

        public IReadOnlyCollection<Match> GetMatchesSummary()
        {
            return _matches
                .OrderBy(m => m.AwayTeamScore + m.HomeTeamScore)
                .ThenByDescending(m => m.StartTime)
                .ToImmutableArray();
        }
    }
}
