using FluentValidation;

namespace GreenSphere.Application.Features.Auth.Commands.ConfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotNull().WithMessage("Email cannot be null.")
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.Token)
            .NotNull().WithMessage("Token cannot be null.")
            .NotEmpty().WithMessage("Token is required");
    }
}