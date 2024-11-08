using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;
public sealed class Verify2FACodeCommand : IRequest<Result<SignInResponseDto>>
{
    public string Code { get; set; } = null!;
}
