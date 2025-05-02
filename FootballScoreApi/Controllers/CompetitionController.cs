using FootballScoreApp.DTOs;
using FootballScoreApp.Features.Competitions.GetMatchesByCompetitionId;
using FootballScoreApp.Features.Competitions.GetStandingsByCompetitionId;
using FootballScoreApp.Features.League.GetAvailableLeagues;
using FootballScoreApp.Features.League.GetLeagueByShortName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CompetitionController : Controller
    {
        private readonly ISender _sender;

        public CompetitionController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetCompetitionByShortName")]
        public async Task<ActionResult<Competiton>> GetCompetitionByShortName(string shortName)
        {
            return Ok(await _sender.Send(new GetCompetitionByShortNameQuery(shortName)));
        }

        [HttpGet("GetAvailableCompetitions")]
        public async Task<ActionResult<CompetitonsResponse>> GetAvailableCompetitions()
        {
            return Ok(await _sender.Send(new GetAvailableCompetitionsQuery()));
        }

        [HttpGet("GetMatchesByCompetitionId")]
        public async Task<ActionResult<MatchesReponse>> GetMatchesByCompetitionId(int id)
        {
            return Ok(await _sender.Send(new GetMatchesByCompetitionIdQuery(id)));
        }

        [HttpGet("GetStandingsByCompetitionId")]
        public async Task<ActionResult<StandingResponse>> GetStandingsByCompetitionId(int id)
        {
            return Ok(await _sender.Send(new GetStandingsByCompetitionIdQuery(id)));
        }
    }
}
