using FluentValidation;
using GreenSphere.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Features.Shorts.Commands.CreateShort;

public sealed class CreateShortCommandValidator : AbstractValidator<CreateShortCommand>
{
    public CreateShortCommandValidator()
    {
        RuleFor(c => c.Video)
            .Must(v => v.Length > 0).WithMessage("Select a short video file.")
            .Must(BeAvalidVideoFile).WithMessage("Not a video file.");

    }

    private static bool BeAvalidVideoFile(IFormFile videoFile)
    {
        var extension = Path.GetExtension(videoFile.FileName).ToLower();
        return FileFormats.AllowedVideoFormats.Contains(extension);
    }
}