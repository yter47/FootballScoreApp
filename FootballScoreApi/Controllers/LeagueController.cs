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
        public async Task<League> GetLeagueByShortName(string shortName)
        {
            return await _leagueService.GetLeagueByShortName(shortName);
        }

        [HttpGet("GetAvailableCompetitions")]
        public async Task<CompetitonsResponse> GetAvailableLeagues()
        {
            return await _leagueService.GetAvailableLeagues();
        }

        [HttpGet("GetMatchesByCompetitionId")]
        public async Task<MatchesReponse> GetMatchesByCompetitionId(int id)
        {
            return await _leagueService.GetMatchesByCompetitionId(id);
        }

        [HttpGet("GetRecentMatches")]
        public async Task<MatchesReponse> GetRecentMatches()
        {
            return await _leagueService.GetRecentMatches();
        }
        
        [HttpGet("GetStandingsByCompetitionId")]
        public async Task<StandingResponse> GetStandingsByCompetitionId(int id)
        {
            return await _leagueService.GetStandingsByCompetitionId(id);
        }
    }
}
