

using FluentValidation;

namespace Application.Features.Auth.Commands.AuthenticationCommands
{
    public class AuthenticationCommandValidator : AbstractValidator<AuthenticationCommand>
    {
        public AuthenticationCommandValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El formato del email no es valido.");
        }
    }
}