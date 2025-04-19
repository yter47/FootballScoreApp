using FootballScoreApp.DTOs;
using FootballScoreApp.Services.IServices;
using MediatR;

namespace FootballScoreApp.Features.Competitions.GetStandingsByCompetitionId
{
    public class GetStandingsByCompetitionIdQueryHandler : IRequestHandler<GetStandingsByCompetitionIdQuery, StandingResponse>
    {

        private readonly ICompetitionService _competitionService;

        public GetStandingsByCompetitionIdQueryHandler(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        public async Task<StandingResponse> Handle(GetStandingsByCompetitionIdQuery request, CancellationToken cancellationToken)
        {
            return await _competitionService.GetStandingsByCompetitionId(request.CompetitionId);
        }
    }
}
