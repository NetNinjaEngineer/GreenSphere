using FluentValidation;

namespace GreenSphere.Application.Features.Auth.Commands.Verify2FaCode;

public class Verify2FaCodeCommandValidator : AbstractValidator<Verify2FaCodeCommand>
{
    public Verify2FaCodeCommandValidator()
    {

        RuleFor(c => c.Code)
            .NotEmpty().WithMessage("Code is required.")
            .NotNull().WithMessage("Code can not be null.")
            .Length(6).WithMessage("Code should be 6 numbers!");
    }
}
