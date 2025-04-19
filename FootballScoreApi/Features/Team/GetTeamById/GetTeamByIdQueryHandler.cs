using FootballScoreApp.Services.IServices;
using MediatR;

namespace FootballScoreApp.Features.Team.GetTeamById
{
    public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, DTOs.Team>
    {
        private readonly ITeamService _teamService;

        public GetTeamByIdQueryHandler(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<DTOs.Team> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            return await _teamService.GetTeamById(request.TeamId);
        }
    }
}
