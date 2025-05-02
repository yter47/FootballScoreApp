using FootballScoreApp.DTOs;
using FootballScoreApp.Features.Authentication.AuthorizeUser;
using FootballScoreApp.Features.Authentication.LoginUser;
using FootballScoreApp.Features.Authentication.RefreshToken;
using FootballScoreApp.Features.Authentication.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult<TokenResponseDto>> RegisterUser([FromBody] RegisterUserCommand command)
        {
            var result = await _sender.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new { Error = result.Error });
            }
            return Ok(result.Value);
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<TokenResponseDto>> LoginUser(LoginUserCommand command)
        {
            var result = await _sender.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new { Error = result.Error });
            }
            return Ok(result.Value);
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenCommand command)
        {
            var result = await _sender.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new { Error = result.Error });
            }
            return Ok(result.Value);
        }

        [Authorize]
        [HttpGet("Authorize")]
        public async Task<ActionResult<TokenResponseDto>> AuthenticatedOnlyEndPoint()
        {
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized();
            }

            var query = new AuthorizeUserQuery(username);
            var result = await _sender.Send(query);

            if (result.IsFailure)
                return BadRequest(new { Error = result.Error });

            return Ok(result.Value);
        }
    }
}
