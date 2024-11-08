﻿using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;

namespace GreenSphere.Application.Interfaces.Identity;
public interface IAuthService
{
    Task<Result<SignUpResponseDto>> RegisterAsync(RegisterCommand command);
    Task<Result<SendCodeConfirmEmailResponseDto>> SendConfirmEmailCodeAsync(SendConfirmEmailCodeCommand command);
    Task<Result<string>> ConfirmEmailAsync(ConfirmEmailCommand command);
    Task LogoutAsync();
    Task<Result<GoogleAuthResponseDto>> GoogleLoginAsync(GoogleLoginCommand command);
    Task<Result<SignInResponseDto>> LoginAsync(LoginCommand command);
    Task<Bases.Result<SignInResponseDto>> RefreshTokenAsync(RefreshTokenCommand command);
    Task<Bases.Result<bool>> RevokeTokenAsync(RevokeTokenCommand command);
    Task<Result<string>> ForgotPasswordAsync(ForgotPasswordCommand command);
    Task<Result<string>> ConfirmForgotPasswordCodeAsync(ConfirmForgotPasswordCodeCommand command);
    Task<Result<string>> Enable2FAAsync(Enable2FACommand command);
    Task<Result<string>> ConfirmEnable2FAAsync(ConfirmEnable2FACommand command);
    Task<Result<SignInResponseDto>> Verify2FACodeAsync(Verify2FACodeCommand command);
    Task<Result<string>> Disable2FAAsync(Disable2FACommand command);
}
