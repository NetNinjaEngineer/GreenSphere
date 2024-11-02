using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;

public sealed class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, Result<SignInResponseDto>>
{
    public async Task<Result<SignInResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        => await authService.LoginAsync(request);
}