
namespace WorldCupScoreBoard
{
    public interface IMatchProvider
    {
        IReadOnlyCollection<Match> Matches { get; }

        Match AddNewMatchForTeams(Team homeTeam, Team awayTeam);
        Match? GetMatchById(Guid id);
        void RemoveMatch(Match match);
    }
}