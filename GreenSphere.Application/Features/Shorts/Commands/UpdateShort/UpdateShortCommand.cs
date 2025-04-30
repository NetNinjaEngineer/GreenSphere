using GreenSphere.Application.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Features.Shorts.Commands.UpdateShort;
public sealed class UpdateShortCommand(
    Guid id,
    string? title,
    string? description,
    IFormFile? video,
    IFormFile? thumbnail,
    bool? isFeatured,
    Guid? shortCategoryId)
    : IRequest<Result<bool>>
{
    public Guid Id { get; private set; } = id;
    public string? Title { get; private set; } = title;
    public string? Description { get; private set; } = description;
    public IFormFile? Video { get; private set; } = video;
    public IFormFile? Thumbnail { get; private set; } = thumbnail;
    public bool? IsFeatured { get; private set; } = isFeatured;
    public Guid? ShortCategoryId { get; private set; } = shortCategoryId;
}