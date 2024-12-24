using FluentValidation;

namespace GreenSphere.Application.Features.Auth.Commands.SendConfirmEmailCode;

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