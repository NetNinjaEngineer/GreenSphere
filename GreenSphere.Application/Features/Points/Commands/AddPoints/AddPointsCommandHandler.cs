using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Points;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Points.Commands.AddPoints;
public sealed class AddPointsCommandHandler(IPointsService service)
    : IRequestHandler<AddPointsCommand, Result<PointsDto>>
{
    public async Task<Result<PointsDto>> Handle(
        AddPointsCommand request, CancellationToken cancellationToken)
        => await service.AddPointsAsync(request);
}
