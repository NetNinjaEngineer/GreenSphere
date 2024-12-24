using GreenSphere.Application.Abstractions;
using GreenSphere.Application.DTOs.Auth;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.SendConfirmEmailCode;

public sealed class SendConfirmEmailCodeCommand : IRequest<Result<SendCodeConfirmEmailResponseDto>>
{
    public string Provider { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
