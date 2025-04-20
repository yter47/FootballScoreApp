using FluentValidation;
using FootballScoreApp.Features.Authentication.RegisterUser;

namespace FootballScoreApp.Validators
{
    internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name can´t be empty");
                
        }
    }
}
