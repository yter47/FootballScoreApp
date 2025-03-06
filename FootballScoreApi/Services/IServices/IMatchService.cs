using FootballScoreApp.DTOs;

namespace FootballScoreApp.Services.IServices
{
    public interface IMatchService
    {
        Task<MatchesReponse> GetRecentMatches();
        Task<MatchesReponse> GetMatchById(int id);
    }
}
