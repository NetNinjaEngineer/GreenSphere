using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Points;
using MediatR;

namespace GreenSphere.Application.Features.Points.Queries.GetPointsHistory;
public sealed class GetPointsHistoryQuery : IRequest<Result<IEnumerable<PointsDto>>>
{
}
