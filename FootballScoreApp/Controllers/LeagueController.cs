using FootballScoreApp.DTOs;
using FootballScoreApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeagueController : Controller
    {
        private readonly ILeagueService _leagueService;
        public LeagueController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        [HttpGet("GetLeagueByShortName")]
        public Task<League> GetLeagueByShortName(string shortName)
        {
            return _leagueService.GetLeagueByShortName(shortName);
        }
        
        [HttpGet("GetAvailableCompetitions")]
        public Task<CompetitonsResponse> GetAvailableLeagues()
        {
            return _leagueService.GetAvailableLeagues();
        }

        [HttpGet("GetMatchesByCompetitionId")]
        public Task<IEnumerable<Match>> GetMatchesByCompetitionId(int id)
        {
            return _leagueService.GetMatchesByCompetitionId(id);
        }
    }
}
