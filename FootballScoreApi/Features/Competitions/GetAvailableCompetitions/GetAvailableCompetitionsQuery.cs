using FootballScoreApp.DTOs;
using MediatR;

namespace FootballScoreApp.Features.League.GetAvailableLeagues
{
    public record GetAvailableCompetitionsQuery : IRequest<CompetitonsResponse>;
}
