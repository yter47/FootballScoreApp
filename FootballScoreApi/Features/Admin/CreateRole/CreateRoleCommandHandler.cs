using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using MediatR;

namespace FootballScoreApp.Features.Roles.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Role?>
    {
        private readonly AppDbContext _context;
        public CreateRoleCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role
            {
                Name = request.Name
            };
            if (role is null)
            {
                return null;
            }

            _context.Add(role);
            await _context.SaveChangesAsync(cancellationToken);

            return role;
        }
    }
}
