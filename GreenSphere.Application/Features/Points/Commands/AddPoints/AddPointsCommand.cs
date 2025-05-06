using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Points;
using GreenSphere.Domain.Enumerations;
using MediatR;

namespace GreenSphere.Application.Features.Points.Commands.AddPoints;
public sealed class AddPointsCommand : IRequest<Result<PointsDto>>
{
    public long Points { get; set; }
    public ActivityType ActivityType { get; set; }
    public string? Description { get; set; }
    public string UserId { get; set; } = null!;
}
