using FootballScoreApp.DTOs;
using MediatR;

namespace FootballScoreApp.Features.Competitions.GetStandingsByCompetitionId
{
    public record GetStandingsByCompetitionIdQuery(int CompetitionId) : IRequest<StandingResponse>;
}
