using FluentValidation;
using GreenSphere.Application.Features.Auth.Requests.Commands;

namespace GreenSphere.Application.Features.Auth.Validators.Commands
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotNull().WithMessage("Email can not be null.")
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(c => c.Password)
                .NotNull().WithMessage("Password cannot be null.")
                .NotEmpty().WithMessage("Password is required")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$")
                .WithMessage("Password must be at least 8 characters long, include at least one lowercase letter, one uppercase letter, one digit, and one special character (e.g., !@#$%^&*).");

        }
    }
}
