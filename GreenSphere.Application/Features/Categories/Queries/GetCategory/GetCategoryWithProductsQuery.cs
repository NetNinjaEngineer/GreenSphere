using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Categories.Queries.GetCategoryWithProducts;

public sealed class GetCategoryWithProductsQuery : IRequest<Result<CategoryWithProductsDto>>
{
    public Guid CategoryId { get; set; }
}