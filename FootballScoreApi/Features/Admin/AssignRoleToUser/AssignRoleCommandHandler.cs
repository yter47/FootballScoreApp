using FootballScoreApp.Entities;
using FootballScoreApp.Repositories.IRepositories;
using MediatR;

namespace FootballScoreApp.Features.Admin.AssignRoleToUser
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, UserRole?>
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;

        public AssignRoleCommandHandler(
            IRepository<Role> roleRepository,
            IUserRoleRepository userRoleRepository,
            IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
        }

        public async Task<UserRole?> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            var role = await _roleRepository.GetByIdAsync(request.RoleId, cancellationToken);

            if (user is null || role is null)
                return null;

            var userRole = await _userRoleRepository.GetUserRoleByUserAndRoleId(user.Id, role.Id);

            if (userRole is not null)
                return userRole;

            var entity = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            _userRoleRepository.Add(entity);
            await _userRoleRepository.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
