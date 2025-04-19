using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Features.Users.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User?>
    {
        private readonly AppDbContext _context;

        public RegisterUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            if(await _context.Users.AnyAsync(u => u.Username.ToLower() == request.username.ToLower()))
            {
                return null;
            }

            var user = new User
            {
                FirstName = request.firstName,
                LastName = request.lastName,
                Username = request.username
            };

            var passwordHash = new PasswordHasher<User>()
                .HashPassword(user, request.password);

            user.PasswordHash = passwordHash;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
