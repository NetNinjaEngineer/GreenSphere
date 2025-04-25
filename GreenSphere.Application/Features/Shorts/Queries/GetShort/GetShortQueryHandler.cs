using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Queries.GetShort;
public sealed class GetShortQueryHandler(IShortsService service)
    : IRequestHandler<GetShortQuery, Result<ShortDto>>
{
    public async Task<Result<ShortDto>> Handle(GetShortQuery request, CancellationToken cancellationToken)
        => await service.GetShortByIdAsync(request.Id);
}
