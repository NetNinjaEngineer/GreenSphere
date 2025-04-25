using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Shorts.Commands.DeleteShort;
public sealed class DeleteShortCommandHandler(IShortsService service)
    : IRequestHandler<DeleteShortCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteShortCommand request, CancellationToken cancellationToken)
        => await service.DeleteShortAsync(request.Id);
}
