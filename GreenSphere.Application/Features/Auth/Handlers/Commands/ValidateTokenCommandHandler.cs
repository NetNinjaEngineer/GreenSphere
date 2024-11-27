using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;
public sealed class ValidateTokenCommandHandler(
    IAuthService authService) : IRequestHandler<ValidateTokenCommand, Result<ValidateTokenResponseDto>>
{
    public async Task<Result<ValidateTokenResponseDto>> Handle(
        ValidateTokenCommand request,
        CancellationToken cancellationToken)
        => await authService.ValidateTokenAsync(request);
}
