using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;

public sealed class RevokeTokenCommand : IRequest<Result<bool>>
{
    public string? Token { get; set; }
}