using FootballScoreApp.DTOs;

namespace FootballScoreApp.Services.IServices
{
    public interface ILeagueService
    {
        Task<CompetitonsResponse> GetAvailableLeagues();
        Task<League> GetLeagueByShortName(string shortName);
        Task<IEnumerable<Match>> GetMatchesByCompetitionId(int id);
        Task<MatchesReponse> GetRecentMatches();
    }
}
