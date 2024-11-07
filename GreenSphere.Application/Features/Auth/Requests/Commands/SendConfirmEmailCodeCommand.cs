using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;

public sealed class SendConfirmEmailCodeCommand : IRequest<Result<SendCodeConfirmEmailResponseDto>>
{
    public string Provider { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
