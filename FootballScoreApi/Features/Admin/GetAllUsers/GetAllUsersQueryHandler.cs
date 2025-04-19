using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Features.Admin.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly AppDbContext _context;

        public GetAllUsersQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .ToListAsync(cancellationToken);
        }
    }
}
