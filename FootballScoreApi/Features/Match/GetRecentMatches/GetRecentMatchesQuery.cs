using FootballScoreApp.DTOs;
using MediatR;

namespace FootballScoreApp.Features.Match.GetRecentMatches
{
    public record GetRecentMatchesQuery : IRequest<MatchesReponse>;
}
