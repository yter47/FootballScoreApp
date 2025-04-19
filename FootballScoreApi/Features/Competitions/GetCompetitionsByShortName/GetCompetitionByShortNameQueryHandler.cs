using FootballScoreApp.Services.IServices;
using MediatR;

namespace FootballScoreApp.Features.League.GetLeagueByShortName
{
    public class GetCompetitionByShortNameQueryHandler : IRequestHandler<GetCompetitionByShortNameQuery, DTOs.Competiton>
    {
        private readonly ICompetitionService _leagueService;

        public GetCompetitionByShortNameQueryHandler(ICompetitionService leagueService)
        {
            _leagueService = leagueService;
        }

        public async Task<DTOs.Competiton> Handle(GetCompetitionByShortNameQuery request, CancellationToken cancellationToken)
        {
            return await _leagueService.GetCompetitionByShortName(request.ShortName);
        }
    }
}
