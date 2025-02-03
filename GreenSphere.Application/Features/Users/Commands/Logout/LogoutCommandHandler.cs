using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.Logout
{
    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly IUserPrivacyService _privacyService;

        public LogoutCommandHandler(IUserPrivacyService privacyService)
        {
            _privacyService = privacyService;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _privacyService.LogoutAsync();
            return true;
        }
    }
}
