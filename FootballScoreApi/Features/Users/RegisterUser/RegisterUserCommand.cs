using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using MediatR;

namespace FootballScoreApp.Features.Users.RegisterUser
{
    public record RegisterUserCommand(string firstName, string lastName, string username, string password) : IRequest<TokenResponseDto?>;

}
