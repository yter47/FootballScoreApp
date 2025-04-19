using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FootballScoreApp.Features.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly AppDbContext _context;

        public CreateUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = request.firstName,
                LastName = request.lastName
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }
    }
}
