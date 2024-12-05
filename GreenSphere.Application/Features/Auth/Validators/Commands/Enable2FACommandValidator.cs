using FluentValidation;
using GreenSphere.Application.Features.Auth.Requests.Commands;

namespace GreenSphere.Application.Features.Auth.Validators.Commands
{
    public class Enable2FACommandValidator : AbstractValidator<Enable2FaCommand>
    {
        public Enable2FACommandValidator()
        {
            RuleFor(c => c.Email)
            .NotNull().WithMessage("Email cannot be null.")
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address.");
        }
    }
}
