using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;
public sealed class FacebookLoginCommand : IRequest<Result<bool>>
{
    public string AccessToken { get; set; } = string.Empty;
}
