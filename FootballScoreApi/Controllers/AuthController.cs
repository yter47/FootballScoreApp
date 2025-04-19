using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Features.Authentication.LoginUser;
using FootballScoreApp.Features.Authentication.RefreshToken;
using FootballScoreApp.Features.Authentication.RegisterUser;
using MediatR;
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
        public async Task<ActionResult<TokenResponseDto?>> RegisterUser(RegisterUserCommand command)
        {
            if (command.Password.Length < 8)
            {
                return BadRequest("Password must be atleast 8 characters long");
            }

            var user = await _sender.Send(command);
            if (user is null)
            {
                return BadRequest("Username already exists");
            }

            return Ok(user);
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<TokenResponseDto>> LoginUser(LoginUserCommand command)
        {
            var response = await _sender.Send(command);
            if (response is null)
            {
                return BadRequest("Username or password was incorrect");
            }

            return Ok(response);
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenCommand command)
        {
            var response = await _sender.Send(command);
            if (response is null)
            {
                return BadRequest("Refresh token is invalid");
            }

            return Ok(response);
        }
    }
}
