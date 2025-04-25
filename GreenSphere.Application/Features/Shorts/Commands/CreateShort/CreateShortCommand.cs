using GreenSphere.Application.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Features.Shorts.Commands.CreateShort;
public sealed class CreateShortCommand : IRequest<Result<Guid>>
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public IFormFile Video { get; set; } = null!;
    public IFormFile? Thumbnail { get; set; }
    public bool IsFeatured { get; set; }
    public Guid ShortCategoryId { get; set; }
}
