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

        public Guid StartMatch(Team homeTeam, Team awayTeam)
        {
            if (_matches.Any(m => m.HomeTeam == homeTeam
                || m.AwayTeam == homeTeam
                || m.HomeTeam == awayTeam
                || m.AwayTeam == awayTeam))
            {
                throw new ArgumentException("Scoreboard already contains a match with the same teams");
            }

            var match = new Match(homeTeam, awayTeam, DateTime.Now);
            _matches.Add(match);
            match.Start();

            return match.Id;
        }

        public IReadOnlyCollection<MatchSummary> GetMatchesSummary()
        {
            return _matches
                .OrderByDescending(m => m.AwayTeamScore + m.HomeTeamScore)
                .ThenByDescending(m => m.StartTime)
                .Select(m => new MatchSummary(m.Id, m.Summary, m.Status))
                .ToImmutableArray();
        }

        public void FinishMatch(Guid id)
        {
            var match = GetMatchById(id);

            match.Finish();
            _matches.Remove(match);
        }

        public void UpdateMatchScore(Guid id, int homeTeamScore, int awayTeamScore)
        {
            var match = GetMatchById(id);

            match.UpdateScores(homeTeamScore, awayTeamScore);
        }

        private Match GetMatchById(Guid id)
        {
            var match = _matches.FirstOrDefault(m => m.Id == id);
            if (match == null)
            {
                throw new ArgumentException($"Match with Id '{id}' cannot be found");
            }

            return match;
        }
    }
}
