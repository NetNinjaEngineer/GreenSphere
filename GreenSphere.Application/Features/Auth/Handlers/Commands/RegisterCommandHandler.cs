using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;
public sealed class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, Result<SignUpResponseDto>>
{
    public async Task<Result<SignUpResponseDto>> Handle(RegisterCommand request,
                                                        CancellationToken cancellationToken)
        => await authService.RegisterAsync(request);
}
