using FluentValidation;

namespace GreenSphere.Application.Features.Shorts.Commands.UpdateShortCategory;

public class UpdateShortCategoryCommandValidator : AbstractValidator<UpdateShortCategoryCommand>
{
    public UpdateShortCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.NameAr)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.NameAr));

        RuleFor(x => x.NameEn)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.NameEn));

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}