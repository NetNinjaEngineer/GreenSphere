using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Points.Queries.GetAvailablePoints;
public sealed class GetAvailablePointsQuery : IRequest<Result<long>>
{
}
