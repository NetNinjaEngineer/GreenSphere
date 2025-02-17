using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

}

