using FootballScoreApp.Abstractions;
using FootballScoreApp.Entities;
using FootballScoreApp.Repositories.IRepositories;
using FootballScoreApp.Services.IServices;
using MediatR;

namespace FootballScoreApp.Features.User.FollowTeam
{
    public class FollowTeamCommandHandler : IRequestHandler<FollowTeamCommand, Result<int?>>
    {
        private readonly IUserRepository userRepository;
        private readonly IUserTeamRepository userTeamRepository;
        private readonly ICurrentUserService currentUserService;

        public FollowTeamCommandHandler(IUserRepository userRepository
            , IUserTeamRepository userTeamRepository
            , ICurrentUserService currentUserService)
        {
            this.userRepository = userRepository;
            this.userTeamRepository = userTeamRepository;
            this.currentUserService = currentUserService;
        }

        public async Task<Result<int?>> Handle(FollowTeamCommand request
            , CancellationToken cancellationToken)
        {
            var userId = currentUserService.UserId;
            var user = await this.userRepository.GetByIdAsync(userId, cancellationToken);
            var isFollowing = await this.userTeamRepository.UserIsFollowing(userId, request.teamId, cancellationToken);
         
            if(isFollowing)
            {
                await this.userTeamRepository.DeleteUserTeam(request.teamId, userId, cancellationToken);
                return Result<int?>.Success(request.teamId);
            }
    
            this.userTeamRepository.Add(new UserTeam { TeamId = request.teamId, UserId = user.Id }, cancellationToken);
            return Result<int?>.Success(request.teamId);
        }
    }
}
