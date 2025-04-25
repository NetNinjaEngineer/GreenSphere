using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Queries.GetAllShortCategories;
public sealed class GetAllShortCategoriesQuery : IRequest<Result<IReadOnlyList<ShortCategoryDto>>>
{
}
