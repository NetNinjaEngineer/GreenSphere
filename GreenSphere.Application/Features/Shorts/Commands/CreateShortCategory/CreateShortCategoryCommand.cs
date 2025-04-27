using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using MediatR;

namespace GreenSphere.Application.Features.ShortCategories.Commands.CreateShortCategory;

public sealed class CreateShortCategoryCommand : IRequest<Result<ShortCategoryDto>>
{
    public string NameAr { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string Description { get; set; } = null!;
}
