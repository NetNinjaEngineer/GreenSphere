using FluentValidation;

namespace GreenSphere.Application.Features.Auth.Commands.Disable2Fa
{
    public class Disable2FaCommandValidator : AbstractValidator<Disable2FaCommand>
    {
        public Disable2FaCommandValidator()
        {
            RuleFor(e => e.Email)
                .NotNull().WithMessage("Email cannot be null.")
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

        }
    }
}
