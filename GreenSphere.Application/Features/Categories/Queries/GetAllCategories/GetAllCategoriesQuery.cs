using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Category;
using GreenSphere.Domain.Utils;
using MediatR;

namespace GreenSphere.Application.Features.Categories.Queries.GetAllCategories;

public sealed class GetAllCategoriesQuery : IRequest<Result<IReadOnlyList<CategoryDto>>>
{
    public CategorySpecParams? CategorySpecParams { get; set; }
}