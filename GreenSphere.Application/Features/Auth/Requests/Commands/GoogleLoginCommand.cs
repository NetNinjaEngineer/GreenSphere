using GreenSphere.Application.Features.Auth.DTOs;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;
public sealed class GoogleLoginCommand : IRequest<Bases.Result<GoogleUserProfile?>>
{
    public string IdToken { get; set; } = string.Empty;
}
