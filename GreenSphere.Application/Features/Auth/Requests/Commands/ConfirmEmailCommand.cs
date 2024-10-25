using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;
public sealed class ConfirmEmailCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
