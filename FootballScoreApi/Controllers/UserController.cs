using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using FootballScoreApp.Features.Users.CreateUser;
using FootballScoreApp.Features.Users.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Identity;
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

        [HttpPost]
        public async Task<ActionResult<int>> CreateUser(CreateUserCommand command)
        {
            return Ok(await _sender.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _sender.Send(new GetUserByIdQuery(id));
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
