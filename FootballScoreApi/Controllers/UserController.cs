using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Features.Users.CreateUser;
using FootballScoreApp.Features.Users.GetUserById;
using FootballScoreApp.Features.Users.LoginUser;
using FootballScoreApp.Features.Users.RefreshToken;
using FootballScoreApp.Features.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<int>> CreateUser(CreateUserCommand command)
        {
            return Ok(await _sender.Send(command));
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _sender.Send(new GetUserByIdQuery(id));
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize]
        [HttpGet("Authorize")]
        public IActionResult AuthenticatedOnlyEndPoint()
        {
            return Ok("Autenticated");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("Admin");
        }
    }
}
