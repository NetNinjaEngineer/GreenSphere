using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Categories.Commands.UpdateCategory;

public sealed class UpdateCategoryCommand : IRequest<Result<Guid>>
{
    public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}