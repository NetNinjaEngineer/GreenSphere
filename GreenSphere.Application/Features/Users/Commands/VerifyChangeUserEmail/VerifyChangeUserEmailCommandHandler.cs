using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.VerifyChangeUserEmail
{
    public sealed class VerifyChangeUserEmailCommandHandler(
        IUserPrivacyService privacyService)
        : IRequestHandler<VerifyChangeUserEmailCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(
            VerifyChangeUserEmailCommand request,
            CancellationToken cancellationToken)
        {
            return await privacyService.VerifyChangeUserEmailAsync(request);
        }
    }
}