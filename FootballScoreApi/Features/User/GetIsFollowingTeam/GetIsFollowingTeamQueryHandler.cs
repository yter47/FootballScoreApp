using FootballScoreApp.Abstractions;
using FootballScoreApp.Entities;
using FootballScoreApp.Repositories.IRepositories;
using FootballScoreApp.Services.IServices;
using MediatR;

namespace FootballScoreApp.Features.User.GetIsFollowingTeam
{
    public class GetIsFollowingTeamQueryHandler : IRequestHandler<GetIsFollowingTeamQuery, Result<bool?>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetIsFollowingTeamQueryHandler(IUserRepository userRepository
            , ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result<bool?>> Handle(GetIsFollowingTeamQuery request, CancellationToken cancellationToken)
        {
            var userId = this._currentUserService.UserId;

            if (userId == 0)
            {
                return Result<bool?>.Failure("User was not found");
            }
            return Result<bool?>.Success(await _userRepository.IsUserFollowingTeamAsync(userId, request.teamId, cancellationToken));
        }
    }
}
