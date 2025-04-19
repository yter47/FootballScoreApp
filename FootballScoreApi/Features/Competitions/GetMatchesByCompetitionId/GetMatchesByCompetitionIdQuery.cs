using FootballScoreApp.DTOs;
using MediatR;

namespace FootballScoreApp.Features.Competitions.GetMatchesByCompetitionId
{
    public record GetMatchesByCompetitionIdQuery(int CompetitionId) : IRequest<MatchesReponse>;
}
