using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Categories.Commands.DeleteCategory;

public sealed class DeleteCategoryCommand : IRequest<Result<bool>>
{
    public Guid CategoryId { get; set; }
}