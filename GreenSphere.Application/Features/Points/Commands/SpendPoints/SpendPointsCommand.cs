using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Points.Commands.SpendPoints;
public sealed class SpendPointsCommand : IRequest<Result<bool>>
{
    public long Points { get; set; }
}
