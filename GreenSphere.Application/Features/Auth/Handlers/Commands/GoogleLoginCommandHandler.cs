using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;
public sealed class GoogleLoginCommandHandler(IAuthService authService) : IRequestHandler<GoogleLoginCommand, Result<GoogleAuthResponseDto>>
{
    public async Task<Result<GoogleAuthResponseDto>> Handle(GoogleLoginCommand request,
                                                            CancellationToken cancellationToken)
        => await authService.GoogleLoginAsync(request);
}
