using GreenSphere.Application.DTOs.Auth;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.GoogleLogin;
public sealed class GoogleLoginCommand : IRequest<Bases.Result<GoogleUserProfile?>>
{
    public string IdToken { get; set; } = string.Empty;
}
