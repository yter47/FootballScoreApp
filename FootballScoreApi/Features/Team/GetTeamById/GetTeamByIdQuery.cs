using MediatR;

namespace FootballScoreApp.Features.Team.GetTeamById
{
    public record GetTeamByIdQuery(int TeamId) : IRequest<DTOs.Team>;
}
