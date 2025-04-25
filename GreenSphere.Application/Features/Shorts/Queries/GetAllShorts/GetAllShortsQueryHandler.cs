using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Queries.GetAllShorts;
public sealed class GetAllShortsQueryHandler(IShortsService service)
    : IRequestHandler<GetAllShortsQuery, Result<IReadOnlyList<ShortDto>>>
{
    public async Task<Result<IReadOnlyList<ShortDto>>> Handle(
        GetAllShortsQuery request, CancellationToken cancellationToken)
        => await service.GetAllShortsAsync();
}
