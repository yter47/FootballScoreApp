using FootballScoreApp.Entities;
using FootballScoreApp.Repositories.IRepositories;
using MediatR;

namespace FootballScoreApp.Features.Admin.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository
                .GetAllAsync(cancellationToken);
        }
    }
}
