using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Queries.GetShortCategory;
public sealed class GetShortCategoryQuery : IRequest<Result<ShortCategoryDto>>
{
    public Guid Id { get; set; }
}
