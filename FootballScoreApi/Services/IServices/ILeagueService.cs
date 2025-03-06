using FootballScoreApp.DTOs;

namespace FootballScoreApp.Services.IServices
{
    public interface ILeagueService
    {
        Task<CompetitonsResponse> GetAvailableLeagues();
        Task<League> GetLeagueByShortName(string shortName);
        Task<MatchesReponse> GetMatchesByCompetitionId(int id);
        Task<StandingResponse> GetStandingsByCompetitionId(int id);
    }
}
