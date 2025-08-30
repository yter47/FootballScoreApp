using FootballScoreApp.Abstractions;
using FootballScoreApp.Entities;
using FootballScoreApp.Repositories.IRepositories;
using MediatR;

namespace FootballScoreApp.Features.User.FollowTeam
{
    public class FollowTeamCommandHandler : IRequestHandler<FollowTeamCommand, Result<int?>>
    {
        private readonly IUserRepository userRepository;
        private readonly IUserTeamRepository userTeamRepository;

        public FollowTeamCommandHandler(IUserRepository userRepository
            , IUserTeamRepository userTeamRepository
            )
        {
            this.userRepository = userRepository;
            this.userTeamRepository = userTeamRepository;
        }

        public async Task<Result<int?>> Handle(FollowTeamCommand request
            , CancellationToken cancellationToken)
        {
            var user = await this.userRepository.GetByIdAsync(request.userId, cancellationToken);
            var isFollowing = await this.userTeamRepository.UserIsFollowing(request, cancellationToken);
         
            if(isFollowing)
            {
                return Result<int?>.Failure($"Already following team with id: {request.teamId}");
            }

            if(user is null)
            {
                return Result<int?>.Failure($"User not found, id: {request.userId}");
            }
    
            this.userTeamRepository.Add(new UserTeam { TeamId = request.teamId, UserId = user.Id }, cancellationToken);
            return Result<int?>.Success(request.teamId);
        }
    }
}
