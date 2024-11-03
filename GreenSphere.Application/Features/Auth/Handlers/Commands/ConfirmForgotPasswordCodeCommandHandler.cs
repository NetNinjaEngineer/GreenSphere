using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands
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
