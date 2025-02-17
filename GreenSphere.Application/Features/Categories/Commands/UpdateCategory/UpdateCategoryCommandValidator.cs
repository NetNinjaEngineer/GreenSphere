using FluentValidation;

namespace GreenSphere.Application.Features.Categories.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryId)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} is required");

        }
    }
}