using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.ChangeUserPassword
{
    public sealed class ChangeUserPasswordCommandHandler(
        IUserService userService)
        : IRequestHandler<ChangeUserPasswordCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(
            ChangeUserPasswordCommand request,
            CancellationToken cancellationToken)
        {
            return await userService.ChangeUserPasswordAsync(request);
        }
    }
}