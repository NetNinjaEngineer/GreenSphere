using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.ChangeUserEmail
{
    public sealed class ChangeUserEmailCommandHandler(
        IUserPrivacyService privacyService)
        : IRequestHandler<ChangeUserEmailCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(
            ChangeUserEmailCommand request,
            CancellationToken cancellationToken)
        {
            return await privacyService.ChangeUserEmailAsync(request);
        }
    }
}