using FluentValidation;
using GreenSphere.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Features.Products.Commands.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotNull().WithMessage("{PropertyName} can not be null")
            .NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(c => c.Description)
            .NotNull().WithMessage("{PropertyName} can not be null")
            .NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(c => c.Price)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(c => c.Image)
            .NotNull().WithMessage("{PropertyName} can not be null")
            .Must(x => x.Length > 0).WithMessage("{PropertyName} is required")
            .Must(BeAValidImageFormat).WithMessage("Not a valid image format");

        RuleFor(c => c.DiscountPercentage)
            .InclusiveBetween(0, 100).WithMessage("{PropertyName} must be between 0 and 100")
            .When(c => c.DiscountPercentage.HasValue);

        RuleFor(c => c.CategoryId)
            .NotEmpty().WithMessage("{PropertyName} is required");

    }

    private static bool BeAValidImageFormat(IFormFile formFile)
    {
        var extension = Path.GetExtension(formFile.FileName);
        return FileFormats.AllowedImageFormats.Contains(extension.ToLowerInvariant());
    }
}