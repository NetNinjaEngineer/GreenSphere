using FluentValidation;

namespace GreenSphere.Application.Features.ShortCategories.Commands.CreateShortCategory;

public class CreateShortCategoryCommandValidator : AbstractValidator<CreateShortCategoryCommand>
{
    public CreateShortCategoryCommandValidator()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty().WithMessage("Arabic name is required")
            .MaximumLength(100).WithMessage("Arabic name cannot exceed 100 characters");

        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage("English name is required")
            .MaximumLength(100).WithMessage("English name cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");
    }
}