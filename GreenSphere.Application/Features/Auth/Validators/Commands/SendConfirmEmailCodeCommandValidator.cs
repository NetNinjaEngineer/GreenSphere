using FluentValidation;
using GreenSphere.Application.Features.Auth.Requests.Commands;

namespace GreenSphere.Application.Features.Auth.Validators.Commands;

public class SendConfirmEmailCodeCommandValidator : AbstractValidator<SendConfirmEmailCodeCommand>
{
    public SendConfirmEmailCodeCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email cannot be null.")
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email address.");
    }
}