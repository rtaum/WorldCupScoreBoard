using System.Collections.Immutable;

namespace WorldCupScoreBoard
{
    public class Scoreboard
    {
        private readonly IMatchProvider _matchProvider;

        public Scoreboard(IMatchProvider matchProvider)
        {
            _matchProvider = matchProvider;
        }

        public Guid StartMatch(Team homeTeam, Team awayTeam)
        {
            if (_matchProvider.Matches.Any(m => m.HomeTeam == homeTeam
                || m.AwayTeam == homeTeam
                || m.HomeTeam == awayTeam
                || m.AwayTeam == awayTeam))
            {
                throw new ArgumentException("Scoreboard already contains a match with the same teams");
            }

            var match = _matchProvider.AddNewMatchForTeams(homeTeam, awayTeam);
            match.Start();

            return match.Id;
        }

        public IReadOnlyCollection<MatchSummary> GetMatchesSummary()
        {
            return _matchProvider.Matches
                .OrderByDescending(m => m.AwayTeamScore + m.HomeTeamScore)
                .ThenByDescending(m => m.StartTime)
                .Select(m => new MatchSummary(m.Id, m.Summary, m.Status))
                .ToImmutableArray();
        }

        public void FinishMatch(Guid id)
        {
            var match = GetMatchById(id);

            match.Finish();
            _matchProvider.RemoveMatch(match);
        }

        public void UpdateMatchScore(Guid id, int homeTeamScore, int awayTeamScore)
        {
            var match = GetMatchById(id);

            match.UpdateScores(homeTeamScore, awayTeamScore);
        }

        private Match GetMatchById(Guid id)
        {
            var match = _matchProvider.GetMatchById(id);
            if (match == null)
            {
                throw new ArgumentException($"Match with Id '{id}' cannot be found");
            }

            return match;
        }
    }
}
