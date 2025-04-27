using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Commands.UpdateShortCategory;

public sealed class UpdateShortCategoryCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
    public string? NameAr { get; set; }
    public string? NameEn { get; set; }
    public string? Description { get; set; }
}