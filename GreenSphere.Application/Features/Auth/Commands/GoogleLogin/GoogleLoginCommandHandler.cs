using GreenSphere.Application.DTOs.Auth;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.GoogleLogin;
public sealed class GoogleLoginCommandHandler(IAuthService authService) : IRequestHandler<GoogleLoginCommand, Bases.Result<GoogleUserProfile?>>
{
    public async Task<Bases.Result<GoogleUserProfile?>> Handle(GoogleLoginCommand request,
                                                            CancellationToken cancellationToken)
        => await authService.GoogleLoginAsync(request);
}
