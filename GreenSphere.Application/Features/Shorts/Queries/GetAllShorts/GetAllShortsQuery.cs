using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Queries.GetAllShorts;
public sealed class GetAllShortsQuery : IRequest<Result<IReadOnlyList<ShortDto>>>
{
}
