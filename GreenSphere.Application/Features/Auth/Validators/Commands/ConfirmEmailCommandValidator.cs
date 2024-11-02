using FluentValidation;
using GreenSphere.Application.Features.Auth.Requests.Commands;

namespace GreenSphere.Application.Features.Auth.Validators.Commands;

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