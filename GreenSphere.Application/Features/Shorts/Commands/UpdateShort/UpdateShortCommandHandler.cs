using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Commands.UpdateShort;
public sealed class UpdateShortCommandHandler(IShortsService service)
    : IRequestHandler<UpdateShortCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateShortCommand request, CancellationToken cancellationToken)
        => await service.UpdateShortAsync(request);
}