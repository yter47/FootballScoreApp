using FootballScoreApp.DTOs;

namespace FootballScoreApp.Services.IServices
{
    public interface ITeamService
    {
        Task<Team> GetTeamById(int id);
    }
}
