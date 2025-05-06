using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Points.Queries.GetAvailablePoints;
public sealed class GetAvailablePointsQueryHandler(IPointsService service)
    : IRequestHandler<GetAvailablePointsQuery, Result<long>>
{
    public async Task<Result<long>> Handle(
        GetAvailablePointsQuery request, CancellationToken cancellationToken) =>
        await service.GetAvailablePointsAsync(request);
}
