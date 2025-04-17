using FootballScoreApp.Entities;
using MediatR;

namespace FootballScoreApp.Features.Users.RegisterUser
{
    public record RegisterUserCommand(int age, string firstName, string lastName, string username, string passwordHash) : IRequest<User>;

}
