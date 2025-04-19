using MediatR;

namespace FootballScoreApp.Features.League.GetLeagueByShortName
{
    public record GetCompetitionByShortNameQuery(string ShortName) : IRequest<DTOs.Competiton>;
}
