using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext context;

        public UserController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateUser(int age, string firstName, string lastName)
        {
            var user = new User
            {
                Age = age,
                FirstName = firstName,
                LastName = lastName
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(user.Id);

        }
    }
}
