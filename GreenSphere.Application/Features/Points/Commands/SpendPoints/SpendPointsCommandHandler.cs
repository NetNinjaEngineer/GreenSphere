using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Points.Commands.SpendPoints;
public sealed class SpendPointsCommandHandler(IPointsService service)
    : IRequestHandler<SpendPointsCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        SpendPointsCommand request, CancellationToken cancellationToken)
        => await service.SpendPointsAsync(request);
}
