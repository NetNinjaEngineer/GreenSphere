using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Queries.GetShortCategory;
public sealed class GetShortCategoryQueryHandler(IShortsService service)
    : IRequestHandler<GetShortCategoryQuery, Result<ShortCategoryDto>>
{
    public async Task<Result<ShortCategoryDto>> Handle(GetShortCategoryQuery request,
        CancellationToken cancellationToken) => await service.GetCategoryByIdAsync(request.Id);
}
