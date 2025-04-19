using FootballScoreApp.Services.IServices;
using MediatR;

namespace FootballScoreApp.Features.Match.GetMatchById
{
    public class GetMatchByIdQueryHandler : IRequestHandler<GetMatchByIdQuery, DTOs.Match>
    {
        private readonly IMatchService _matchService;

        public GetMatchByIdQueryHandler(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<DTOs.Match> Handle(GetMatchByIdQuery request, CancellationToken cancellationToken)
        {
            return await _matchService.GetMatchById(request.MatchId);
        }
    }
}
