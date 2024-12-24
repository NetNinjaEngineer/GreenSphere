using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.ConfirmForgotPasswordCode
{
    public class ConfirmForgotPasswordCodeCommandHandler :
        IRequestHandler<ConfirmForgotPasswordCodeCommand, Result<string>>
    {

        private readonly IAuthService _authService;

        public ConfirmForgotPasswordCodeCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result<string>> Handle(ConfirmForgotPasswordCodeCommand request, CancellationToken cancellationToken)
        {
            return await _authService.ConfirmForgotPasswordCodeAsync(request);
        }
    }
}
