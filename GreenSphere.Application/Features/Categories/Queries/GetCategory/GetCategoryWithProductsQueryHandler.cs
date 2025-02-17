using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Categories.Queries.GetCategoryWithProducts
{
    public sealed class GetCategoryWithProductsQueryHandler(IProductsService service)
        : IRequestHandler<GetCategoryWithProductsQuery, Result<CategoryWithProductsDto>>
    {
        public async Task<Result<CategoryWithProductsDto>> Handle(GetCategoryWithProductsQuery request, CancellationToken cancellationToken)
            => await service.GetCategoryWithProductsAsync(request.CategoryId);
    }
}