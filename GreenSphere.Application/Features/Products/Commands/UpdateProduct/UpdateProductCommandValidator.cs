using FluentValidation;
using GreenSphere.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Features.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotNull().WithMessage("{PropertyName} cannot be null")
            .NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(c => c.Description)
            .NotNull().WithMessage("{PropertyName} cannot be null")
            .NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(c => c.Price)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(c => c.Image)
            .Must(BeAValidImageFormat).WithMessage("{PropertyName} is not a valid image format")
            .When(c => c.Image != null);

        RuleFor(c => c.DiscountPercentage)
            .InclusiveBetween(0, 100).WithMessage("{PropertyName} must be between 0 and 100")
            .When(c => c.DiscountPercentage.HasValue);

        RuleFor(c => c.CategoryId)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .When(c => c.CategoryId.HasValue);
    }

    private static bool BeAValidImageFormat(IFormFile? formFile)
    {
        var extension = Path.GetExtension(formFile?.FileName);
        return FileFormats.AllowedImageFormats.Contains(extension?.ToLowerInvariant());
    }
}