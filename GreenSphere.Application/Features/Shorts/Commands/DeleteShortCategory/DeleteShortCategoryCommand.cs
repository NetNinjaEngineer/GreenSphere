using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Commands.DeleteShortCategory;
public sealed class DeleteShortCategoryCommand : IRequest<Result<bool>>
{
    public Guid CategoryId { get; set; }
}
