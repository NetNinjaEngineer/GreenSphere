using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;
public sealed class GoogleLoginCommandHandler(IAuthService authService) : IRequestHandler<GoogleLoginCommand, Bases.Result<GoogleUserProfile?>>
{
    public async Task<Bases.Result<GoogleUserProfile?>> Handle(GoogleLoginCommand request,
                                                            CancellationToken cancellationToken)
        => await authService.GoogleLoginAsync(request);
}
