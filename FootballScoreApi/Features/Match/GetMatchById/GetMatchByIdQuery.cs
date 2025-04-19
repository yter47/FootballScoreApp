using MediatR;

namespace FootballScoreApp.Features.Match.GetMatchById
{
    public record GetMatchByIdQuery(int MatchId) : IRequest<DTOs.Match>;
}
