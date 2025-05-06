using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Points;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Points.Queries.GetPointsSummary;

public sealed class GetPointsSummaryQueryHandler(IPointsService service)
    : IRequestHandler<GetPointsSummaryQuery, Result<PointsSummaryDto?>>
{
    public async Task<Result<PointsSummaryDto?>> Handle(
        GetPointsSummaryQuery request,
        CancellationToken cancellationToken) => await service.GetPointsSummaryAsync(request);
}