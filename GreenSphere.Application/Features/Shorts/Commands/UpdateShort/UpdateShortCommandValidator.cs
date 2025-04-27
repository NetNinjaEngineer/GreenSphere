using FluentValidation;

namespace GreenSphere.Application.Features.Shorts.Commands.UpdateShort;
public class UpdateShortCommandValidator : AbstractValidator<UpdateShortCommand>
{
    public UpdateShortCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Title)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Title));

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => x.Description != null);

    }
}