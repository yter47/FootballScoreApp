using FootballScoreApp.DTOs;
using FootballScoreApp.Services.IServices;
using MediatR;

namespace FootballScoreApp.Features.Competitions.GetMatchesByCompetitionId
{
    public class GetMatchesByCompetitionIdQueryHandler : IRequestHandler<GetMatchesByCompetitionIdQuery, MatchesReponse>
    {

        private readonly ICompetitionService _competitionService;

        public GetMatchesByCompetitionIdQueryHandler(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        public async Task<MatchesReponse> Handle(GetMatchesByCompetitionIdQuery request, CancellationToken cancellationToken)
        {
            return await _competitionService.GetMatchesByCompetitionId(request.CompetitionId);
        }
    }
}
