using GreenSphere.Application.Abstractions;
using GreenSphere.Application.DTOs.Auth;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.SendConfirmEmailCode;
public sealed class SendConfirmEmailCodeCommandHandler(IAuthService authService)
    : IRequestHandler<SendConfirmEmailCodeCommand, Result<SendCodeConfirmEmailResponseDto>>
{
    public async Task<Result<SendCodeConfirmEmailResponseDto>> Handle(SendConfirmEmailCodeCommand request,
                                                                      CancellationToken cancellationToken)
        => await authService.SendConfirmEmailCodeAsync(request);
}
