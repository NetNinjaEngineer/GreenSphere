using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Commands.CreateShortCategory;

public sealed class CreateShortCategoryCommandHandler(IShortsService service)
    : IRequestHandler<CreateShortCategoryCommand, Result<ShortCategoryDto>>
{
    public async Task<Result<ShortCategoryDto>> Handle(
        CreateShortCategoryCommand request,
        CancellationToken cancellationToken)
        => await service.CreateCategoryAsync(request);
}