using FluentValidation;

namespace GreenSphere.Application.Features.Users.Commands.VerifyChangeUserEmail
{
    public sealed class VerifyChangeUserEmailCommandValidation : AbstractValidator<VerifyChangeUserEmailCommand>
    {
        public VerifyChangeUserEmailCommandValidation()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .Length(6).WithMessage("Code must be 6 digits.");
        }
    }
}