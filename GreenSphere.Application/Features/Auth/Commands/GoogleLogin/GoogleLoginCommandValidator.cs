using FluentValidation;

namespace GreenSphere.Application.Features.Auth.Commands.GoogleLogin;
public sealed class GoogleLoginCommandValidator : AbstractValidator<GoogleLoginCommand>
{
    public GoogleLoginCommandValidator()
    {
        RuleFor(x => x.IdToken)
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .Must(BeValidJwtFormat).WithMessage("Token must be in valid JWT format");
    }

    private static bool BeValidJwtFormat(string token) => token.Split(".").Length == 3;
}
