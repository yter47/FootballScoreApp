using FootballScoreApp.Entities;
using FootballScoreApp.Repositories.IRepositories;
using MediatR;

namespace FootballScoreApp.Features.Roles.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Role?>
    {
        private readonly IRepository<Role> _roleRepository;
        public CreateRoleCommandHandler(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role?> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role
            {
                Name = request.Name
            };

            _roleRepository.Add(role, cancellationToken);
            await _roleRepository.SaveChangesAsync(cancellationToken);

            return role;
        }
    }
}
