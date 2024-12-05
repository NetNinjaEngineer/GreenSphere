using FluentValidation;
using GreenSphere.Application.Features.Auth.Requests.Commands;


namespace GreenSphere.Application.Features.Auth.Validators.Commands
{
    public class ConfirmEnable2FaCommandValidator : AbstractValidator<ConfirmEnable2FaCommand>
    {
        public ConfirmEnable2FaCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotNull().WithMessage("Email can not be null.")
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(c => c.Code)
                .NotEmpty().WithMessage("Code is required.")
                .NotNull().WithMessage("Code can not be null.")
                .Length(6).WithMessage("Code should be 6 numbers!");



        }
    }

}
