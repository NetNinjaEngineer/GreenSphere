using GreenSphere.Application.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Features.Shorts.Commands.UpdateShort;
public sealed class UpdateShortCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IFormFile? Video { get; set; }
    public IFormFile? Thumbnail { get; set; }
    public bool? IsFeatured { get; set; }
    public Guid? ShortCategoryId { get; set; }
}