using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Commands.CreateShort;
public sealed class CreateShortCommandHandler(IShortsService service)
    : IRequestHandler<CreateShortCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateShortCommand request, CancellationToken cancellationToken)
        => await service.CreateShortAsync(request);
}
