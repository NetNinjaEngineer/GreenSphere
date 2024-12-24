using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Infrastructure;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.FacebookLogin;
public sealed class FacebookLoginCommandHandler(IFacebookClientApi facebookClient) : IRequestHandler<FacebookLoginCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(FacebookLoginCommand request, CancellationToken cancellationToken)
        => await facebookClient.AuthenticateAsync(request.AccessToken);
}
