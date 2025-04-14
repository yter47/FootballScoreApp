using MediatR;

namespace FootballScoreApp.Features.Users.CreateUser
{
    public record CreateUserCommand(int age, string firstName, string lastName) : IRequest<int>;

}
