using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;

public sealed class Verify2FaCodeCommandHandler(IAuthService authService)
    : IRequestHandler<Verify2FaCodeCommand, Result<SignInResponseDto>>
{
    public async Task<Result<SignInResponseDto>> Handle(Verify2FaCodeCommand request,
        CancellationToken cancellationToken)
        => await authService.Verify2FaCodeAsync(request);
}