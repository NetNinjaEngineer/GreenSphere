using GreenSphere.Application.Abstractions;
using GreenSphere.Application.DTOs.Auth;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.Register;
public sealed class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, Result<SignUpResponseDto>>
{
    public async Task<Result<SignUpResponseDto>> Handle(RegisterCommand request,
                                                        CancellationToken cancellationToken)
        => await authService.RegisterAsync(request);
}
