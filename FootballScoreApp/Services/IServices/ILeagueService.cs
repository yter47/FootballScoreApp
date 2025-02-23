using FootballScoreApp.Entities;

namespace FootballScoreApp.Services.IServices
{
    public interface ILeagueService
    {
        Task<League> GetLeagueByShortName(string shortName);
    }
}
