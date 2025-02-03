using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.ChangeUserPassword
{
    public sealed class ChangeUserPasswordCommandHandler(
        IUserPrivacyService privacyService)
        : IRequestHandler<ChangeUserPasswordCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(
            ChangeUserPasswordCommand request,
            CancellationToken cancellationToken)
        {
            return await privacyService.ChangeUserPasswordAsync(request);
        }
    }
}