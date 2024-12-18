﻿using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<string>>
    {
        private readonly IAuthService _authService;

        public ForgotPasswordCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _authService.ForgotPasswordAsync(request);
        }
    }
}
