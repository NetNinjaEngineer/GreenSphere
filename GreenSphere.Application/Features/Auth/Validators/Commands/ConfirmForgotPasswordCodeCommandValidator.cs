
using FluentValidation;
using GreenSphere.Application.Features.Auth.Requests.Commands;

namespace GreenSphere.Application.Features.Auth.Validators.Commands
{
    public class ConfirmForgotPasswordCodeCommandValidator : AbstractValidator<ConfirmForgotPasswordCodeCommand>
    {
        public ConfirmForgotPasswordCodeCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotNull().WithMessage("Email can not be null.")
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(c => c.Code)
                .NotEmpty().WithMessage("Code is required.")
                .NotNull().WithMessage("Code can not be null.")
                .Length(6).WithMessage("Code should be 6 numbers!");

            RuleFor(c => c.NewPassword)
                .NotNull().WithMessage("Password cannot be null.")
                .NotEmpty().WithMessage("Password is required")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$")
                .WithMessage("Password must be at least 8 characters long, include at least one lowercase letter, one uppercase letter, one digit, and one special character (e.g., !@#$%^&*).")
                .Equal(x => x.ConfirmPassword)
                .WithMessage("Passwords do not match.");
        }
    }
}
