using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using MediatR;

namespace FootballScoreApp.Features.Authentication.RegisterUser
{
    public record RegisterUserCommand(string FirstName, string LastName, string Username, string Password, string ConfirmPassword) : IRequest<TokenResponseDto?>;

}
