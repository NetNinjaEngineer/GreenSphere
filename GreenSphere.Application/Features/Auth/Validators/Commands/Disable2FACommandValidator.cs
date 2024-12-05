using FluentValidation;
using GreenSphere.Application.Features.Auth.Requests.Commands;

namespace GreenSphere.Application.Features.Auth.Validators.Commands
{
    public class Disable2FACommandValidator : AbstractValidator<Disable2FaCommand>
    {
        public Disable2FACommandValidator()
        {
            RuleFor(e => e.Email)
                .NotNull().WithMessage("Email cannot be null.")
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

        }
    }
}
