using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Commands.UpdateShortCategory;

public sealed class UpdateShortCategoryCommandHandler(IShortsService service)
    : IRequestHandler<UpdateShortCategoryCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateShortCategoryCommand request, CancellationToken cancellationToken)
        => await service.UpdateCategoryAsync(request);
}