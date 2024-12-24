using FluentValidation;

namespace GreenSphere.Application.Features.Auth.Commands.FacebookLogin
{
    public class FacebookLoginCommandValidator : AbstractValidator<FacebookLoginCommand>
    {
        public FacebookLoginCommandValidator()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage("{PropertyName} can not be empty.");

        }
    }
}
