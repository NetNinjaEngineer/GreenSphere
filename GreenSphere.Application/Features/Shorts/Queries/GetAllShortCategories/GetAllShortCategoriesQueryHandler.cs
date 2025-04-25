using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Queries.GetAllShortCategories;
public sealed class GetAllShortCategoriesQueryHandler(IShortsService service)
    : IRequestHandler<GetAllShortCategoriesQuery, Result<IReadOnlyList<ShortCategoryDto>>>
{
    public async Task<Result<IReadOnlyList<ShortCategoryDto>>> Handle(
        GetAllShortCategoriesQuery request, CancellationToken cancellationToken)
        => await service.GetAllCategoriesAsync();
}
