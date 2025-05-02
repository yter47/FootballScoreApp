using FootballScoreApp.DTOs;
using FootballScoreApp.Features.Team.GetTeamById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TeamController : Controller
    {
        private readonly ISender _sender;

        public TeamController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetTeamById")]
        public async Task<ActionResult<Team>> GetTeamById(int id)
        {
            return Ok(await _sender.Send(new GetTeamByIdQuery(id)));
        }
    }
}
