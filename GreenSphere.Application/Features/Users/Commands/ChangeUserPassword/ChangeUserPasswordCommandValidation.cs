using FluentValidation;

namespace GreenSphere.Application.Features.Users.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordCommandValidation : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidation()
        {
            RuleFor(x => x.CurrentPassword)
                .NotNull().WithMessage("Password cannot be null.")
                .NotEmpty().WithMessage("Password is required");

            RuleFor(x => x.NewPassword)
                .NotNull().WithMessage("Password cannot be null.")
                .NotEmpty().WithMessage("Password is required")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$")
                .WithMessage("Password must be at least 8 characters long, include at least one lowercase letter, one uppercase letter, one digit, and one special character (e.g., !@#$%^&*).")
                .Equal(x => x.ConfirmNewPassword)
                .WithMessage("Passwords do not match.");


        }
    }
}
