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

        public IReadOnlyCollection<Match> GetMatchesSummary()
        {
            return _matches
                .OrderBy(m => m.AwayTeamScore + m.HomeTeamScore)
                .ThenByDescending(m => m.StartTime)
                .ToImmutableArray();
        }
    }
}
