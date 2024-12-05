using FluentValidation;
using GreenSphere.Application.Features.Auth.Requests.Commands;

namespace GreenSphere.Application.Features.Auth.Validators.Commands
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("{PropertyName} can not be empty.")
                .Must(BeValidJwtFormat).WithMessage("Token must be in valid JWT format");
        }

        private static bool BeValidJwtFormat(string token) => token.Split(".").Length == 3;
    }
}
