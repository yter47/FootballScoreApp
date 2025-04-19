using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Features.Admin.AssignRoleToUser
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, UserRole?>
    {
        private readonly AppDbContext _context;

        public AssignRoleCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserRole?> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            var role = await _context.Roles.FindAsync(request.RoleId);

            if (user is null || role is null)
                return null;

            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == user.Id && ur.RoleId == role.Id);

            if (userRole is not null)
                return userRole;

            var entity = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            _context.UserRoles.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

    }
}
