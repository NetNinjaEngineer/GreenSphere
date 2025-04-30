using FluentValidation;
using GreenSphere.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Features.Shorts.Commands.CreateShort;

public sealed class CreateShortCommandValidator : AbstractValidator<CreateShortCommand>
{
    public CreateShortCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(c => c.Video)
            .Must(v => v.Length > 0).WithMessage("Select a short video file.")
            .Must(BeAvalidVideoFile).WithMessage("Not a video file.");

        RuleFor(x => x.Thumbnail)
            .Must(BeAValidImage).When(x => x.Thumbnail != null)
            .WithMessage("Invalid thumbnail format. Only JPG, JPEG and PNG are allowed.");

    }

    private static bool BeAvalidVideoFile(IFormFile videoFile)
    {
        var extension = Path.GetExtension(videoFile.FileName).ToLower();
        return FileFormats.AllowedVideoFormats.Contains(extension);
    }

    private static bool BeAValidImage(IFormFile? file)
    {
        var extension = Path.GetExtension(file!.FileName).ToLower();
        return FileFormats.AllowedImageFormats.Contains(extension);
    }
}