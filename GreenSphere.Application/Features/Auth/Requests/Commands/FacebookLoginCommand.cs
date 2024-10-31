using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;
public sealed class FacebookLoginCommand : IRequest<Result<bool>>
{
    public string AccessToken { get; set; } = string.Empty;
}
