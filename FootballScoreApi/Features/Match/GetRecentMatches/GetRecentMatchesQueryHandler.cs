using FootballScoreApp.DTOs;
using FootballScoreApp.Services.IServices;
using MediatR;

namespace FootballScoreApp.Features.Match.GetRecentMatches
{
    public class GetRecentMatchesQueryHandler : IRequestHandler<GetRecentMatchesQuery, MatchesReponse>
    {
        private readonly IMatchService _matchService;
        public GetRecentMatchesQueryHandler(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<MatchesReponse> Handle(GetRecentMatchesQuery request, CancellationToken cancellationToken)
        {
            return await _matchService.GetRecentMatches();
        }
    }
}
