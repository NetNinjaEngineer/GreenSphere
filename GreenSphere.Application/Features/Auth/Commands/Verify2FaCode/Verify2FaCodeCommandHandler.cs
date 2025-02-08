using GreenSphere.Application.Abstractions;
using GreenSphere.Application.DTOs.Auth;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.Verify2FaCode;

public sealed class Verify2FaCodeCommandHandler(IAuthService authService)
    : IRequestHandler<Verify2FaCodeCommand, Result<SignInResponseDto>>
{
    public async Task<Result<SignInResponseDto>> Handle(Verify2FaCodeCommand request,
        CancellationToken cancellationToken)
        => await authService.Verify2FaCodeAsync(request);
}