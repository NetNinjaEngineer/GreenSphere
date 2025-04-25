using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Commands.DeleteShortCategory;
public sealed class DeleteShortCategoryCommandHandler(IShortsService service)
    : IRequestHandler<DeleteShortCategoryCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteShortCategoryCommand request, CancellationToken cancellationToken)
        => await service.DeleteCategoryAsync(request.CategoryId);
}
