using FluentValidation;
using GreenSphere.Application.Features.Auth.Requests.Commands;

namespace GreenSphere.Application.Features.Auth.Validators.Commands
{
    public class Verify2FACodeCommandValidator : AbstractValidator<Verify2FaCodeCommand>
    {
        public Verify2FACodeCommandValidator()
        {

            RuleFor(c => c.Code)
                .NotEmpty().WithMessage("Code is required.")
                .NotNull().WithMessage("Code can not be null.")
                .Length(6).WithMessage("Code should be 6 numbers!");
        }
    }
}
