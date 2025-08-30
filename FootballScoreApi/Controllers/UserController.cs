using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Features.User.FollowTeam;
using FootballScoreApp.Features.Users.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<User>> GetUserById(GetUserByIdQuery query)
        {
            var result = await _sender.Send(query);

            if (result.IsFailure)
            {
                return BadRequest(new { Error = result.Error });
            }
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("Admin");
        }

        [HttpPost("FollowTeam")]
        public async Task<ActionResult<int>> FollowTeam(FollowTeamCommand command)
        {
            var result = await _sender.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new { Error = result.Error });
            }
            return Ok(result.Value);
        }
    }
}
