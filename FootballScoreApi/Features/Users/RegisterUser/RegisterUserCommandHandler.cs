using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using Microsoft.AspNetCore.Identity;

namespace FootballScoreApp.Features.Users.RegisterUser
{
    public class RegisterUserCommandHandler
    {
        private readonly AppDbContext _context;

        public RegisterUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Age = request.age,
                FirstName = request.firstName,
                LastName = request.lastName,
                Username = request.username
            };

            var passwordHash = new PasswordHasher<User>()
                .HashPassword(user, request.passwordHash);

            user.PasswordHash = passwordHash;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
