using FootballScoreApp.DTOs;

namespace FootballScoreApp.Services.IServices
{
    public interface ICompetitionService
    {
        Task<CompetitonsResponse> GetAvailableLeagues();
        Task<Competiton> GetCompetitionByShortName(string shortName);
        Task<MatchesReponse> GetMatchesByCompetitionId(int id);
        Task<StandingResponse> GetStandingsByCompetitionId(int id);
    }
}
