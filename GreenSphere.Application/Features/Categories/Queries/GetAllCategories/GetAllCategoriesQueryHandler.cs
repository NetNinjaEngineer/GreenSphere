using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Category;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Categories.Queries.GetAllCategories
{
    public sealed class GetAllCategoriesQueryHandler(IProductsService service)
        : IRequestHandler<GetAllCategoriesQuery, Result<IReadOnlyList<CategoryDto>>>
    {
        public async Task<Result<IReadOnlyList<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            => await service.GetAllCategoriesAsync(request.CategorySpecParams);
    }
}