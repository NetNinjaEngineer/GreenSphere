using FluentValidation;

namespace GreenSphere.Application.Features.Auth.Commands.Enable2Fa;

public class Enable2FaCommandValidator : AbstractValidator<Enable2FaCommand>
{
    public Enable2FaCommandValidator()
    {
        RuleFor(c => c.Email)
        .NotNull().WithMessage("Email cannot be null.")
        .NotEmpty().WithMessage("Email is required")
        .EmailAddress().WithMessage("Invalid email address.");
    }
}
