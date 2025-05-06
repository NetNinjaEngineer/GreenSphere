using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Points;
using MediatR;

namespace GreenSphere.Application.Features.Points.Queries.GetPointsSummary;

public sealed class GetPointsSummaryQuery : IRequest<Result<PointsSummaryDto?>>
{

}