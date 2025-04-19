using FootballScoreApp.Entities;
using FootballScoreApp.Features.Admin.AssignRoleToUser;
using FootballScoreApp.Features.Roles.CreateRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = "Admin")]
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

            if (role is null)
            {
                return BadRequest("Role already exists");
            }

            return Ok(role);
        }

        [HttpPost("AssignRoleToUser")]
        public async Task<ActionResult<UserRole?>> AssignUserToRole(AssignRoleCommand command)
        {
            var userRole = await _sender.Send(command);

            if (userRole is null)
            {
                return BadRequest("Role or User does not exist");
            }

            return Ok(userRole);
        }
    }
}
