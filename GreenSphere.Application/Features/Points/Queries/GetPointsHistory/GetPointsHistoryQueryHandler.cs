using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Points;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Points.Queries.GetPointsHistory;
public sealed class GetPointsHistoryQueryHandler(IPointsService service)
    : IRequestHandler<GetPointsHistoryQuery, Result<IEnumerable<PointsDto>>>
{
    public async Task<Result<IEnumerable<PointsDto>>> Handle(GetPointsHistoryQuery request,
        CancellationToken cancellationToken)
        => await service.GetPointsHistoryAsync(request);
}
