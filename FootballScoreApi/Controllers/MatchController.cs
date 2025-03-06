using FootballScoreApp.DTOs;
using FootballScoreApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;
        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet("GetRecentMatches")]
        public async Task<MatchesReponse> GetRecentMatches()
        {
            return await _matchService.GetRecentMatches();
        }

        [HttpGet("GetMatchById")]
        public async Task<Match> GetMatchById(int id)
        {
            return await _matchService.GetMatchById(id);
        }
    }
}
