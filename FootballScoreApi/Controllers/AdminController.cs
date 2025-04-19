using FootballScoreApp.Entities;
using FootballScoreApp.Features.Roles.CreateRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ISender _sender;

        public AdminController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("CreateRole")]
        public async Task<ActionResult<Role?>> CreateRole(CreateRoleCommand command)
        {
            var role = await _sender.Send(command);

            if(role is null)
            {
                BadRequest("Role already exists");
            }

            return Ok(role);
        }
    }
}
