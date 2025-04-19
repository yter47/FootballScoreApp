using FootballScoreApp.DTOs;
using FootballScoreApp.Services.IServices;
using MediatR;

namespace FootballScoreApp.Features.League.GetAvailableLeagues
{
    public class GetAvailableCompetitionsQueryHandler : IRequestHandler<GetAvailableCompetitionsQuery, CompetitonsResponse>
    {
        private readonly ICompetitionService _leagueService;

        public GetAvailableCompetitionsQueryHandler(ICompetitionService leagueService)
        {
            _leagueService = leagueService;
        }

        public async Task<CompetitonsResponse> Handle(GetAvailableCompetitionsQuery request, CancellationToken cancellationToken)
        {
            return await _leagueService.GetAvailableLeagues();
        }
    }
}
