using FluentValidation;
using GreenSphere.Application.Features.Auth.Requests.Commands;

namespace GreenSphere.Application.Features.Auth.Validators.Commands
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
