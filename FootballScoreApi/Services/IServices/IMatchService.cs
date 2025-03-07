using FootballScoreApp.DTOs;

namespace FootballScoreApp.Services.IServices
{
    public interface IMatchService
    {
        Task<MatchesReponse> GetRecentMatches();
        Task<Match> GetMatchById(int id);
        Task<Team> GetTeamById(int id);
    }
}
