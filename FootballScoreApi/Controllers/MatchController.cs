using FootballScoreApp.DTOs;
using FootballScoreApp.Features.Match.GetMatchById;
using FootballScoreApp.Features.Match.GetRecentMatches;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MatchController : Controller
    {
        private readonly ISender _sender;

        public MatchController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetRecentMatches")]
        public async Task<ActionResult<MatchesReponse>> GetRecentMatches()
        {
            return Ok(await _sender.Send(new GetRecentMatchesQuery()));
        }

        [HttpGet("GetMatchById")]
        public async Task<ActionResult<Match>> GetMatchById(int id)
        {
            return Ok(await _sender.Send(new GetMatchByIdQuery(id)));
        }
    }
}
