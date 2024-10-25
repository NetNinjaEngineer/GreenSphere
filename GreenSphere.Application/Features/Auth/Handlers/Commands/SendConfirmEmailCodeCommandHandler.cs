using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;
public sealed class SendConfirmEmailCodeCommandHandler(IAuthService authService)
    : IRequestHandler<SendConfirmEmailCodeCommand, Result<SendCodeConfirmEmailResponseDto>>
{
    public async Task<Result<SendCodeConfirmEmailResponseDto>> Handle(SendConfirmEmailCodeCommand request,
                                                                      CancellationToken cancellationToken)
        => await authService.SendConfirmEmailCodeAsync(request.Email);
}
