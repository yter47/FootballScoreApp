using FootballScoreApp.Entities;
using FootballScoreApp.Features.Users.GetUserById;
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
