using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Infrastructure;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;
public sealed class FacebookLoginCommandHandler(IFacebookClientApi facebookClient) : IRequestHandler<FacebookLoginCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(FacebookLoginCommand request, CancellationToken cancellationToken)
        => await facebookClient.AuthenticateAsync(request.AccessToken);
}
