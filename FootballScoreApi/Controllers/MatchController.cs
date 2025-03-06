using FootballScoreApp.DTOs;
using FootballScoreApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoreApp.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;
        public MatchController(IMatchService matchService)
        {
            _matchService = matchService
        }

        [HttpGet("GetRecentMatches")]
        public async Task<MatchesReponse> GetRecentMatches()
        {
            return await _matchService.GetRecentMatches();
        }

        [HttpGet("GetMatchById")]
        public async Task<MatchesReponse> GetMatchById()
        {
            return await _matchService.GetMatchById();
        }
    }
}
