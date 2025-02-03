using FluentValidation;

namespace GreenSphere.Application.Features.Users.Commands.ChangeUserEmail
{
    public sealed class ChangeUserEmailCommandValidation : AbstractValidator<ChangeUserEmailCommand>
    {
        public ChangeUserEmailCommandValidation()
        {
            RuleFor(x => x.NewEmail)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required.");
        }
    }
}