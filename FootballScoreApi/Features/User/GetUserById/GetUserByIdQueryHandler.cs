using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using MediatR;

namespace FootballScoreApp.Features.Users.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User?>
    {
        private readonly AppDbContext _context;

        public GetUserByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id, cancellationToken);
            return user;
        }
    }
}
