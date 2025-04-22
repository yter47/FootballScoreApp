using FootballScoreApp.Entities;
using FootballScoreApp.Features.Users.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        public async Task<ActionResult<User>> GetUserById(GetUserByIdQuery query)
        {
            var result = await _sender.Send(query);

            if (result.IsFailure)
            {
                return BadRequest(new { Error = result.Error });
            }
            return Ok(result.Value);
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
