using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.DeleteAccount;
public sealed class DeleteAccountCommandHandler(IUserService service)
    : IRequestHandler<DeleteAccountCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        DeleteAccountCommand request, CancellationToken cancellationToken)
        => await service.DeleteMyAccountAsync();
}
