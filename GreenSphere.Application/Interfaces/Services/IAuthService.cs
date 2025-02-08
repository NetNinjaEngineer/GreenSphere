using GreenSphere.Application.Abstractions;
using GreenSphere.Application.DTOs.Auth;
using GreenSphere.Application.Features.Auth.Commands.ConfirmEmail;
using GreenSphere.Application.Features.Auth.Commands.ConfirmEnable2FA;
using GreenSphere.Application.Features.Auth.Commands.ConfirmForgotPasswordCode;
using GreenSphere.Application.Features.Auth.Commands.Disable2Fa;
using GreenSphere.Application.Features.Auth.Commands.Enable2Fa;
using GreenSphere.Application.Features.Auth.Commands.ForgotPassword;
using GreenSphere.Application.Features.Auth.Commands.GoogleLogin;
using GreenSphere.Application.Features.Auth.Commands.Login;
using GreenSphere.Application.Features.Auth.Commands.RefreshToken;
using GreenSphere.Application.Features.Auth.Commands.Register;
using GreenSphere.Application.Features.Auth.Commands.RevokeToken;
using GreenSphere.Application.Features.Auth.Commands.SendConfirmEmailCode;
using GreenSphere.Application.Features.Auth.Commands.ValidateToken;
using GreenSphere.Application.Features.Auth.Commands.Verify2FaCode;

namespace GreenSphere.Application.Interfaces.Services;

public interface IAuthService
{
    Task<Result<SignUpResponseDto>> RegisterAsync(RegisterCommand command);
    Task<Result<SendCodeConfirmEmailResponseDto>> SendConfirmEmailCodeAsync(SendConfirmEmailCodeCommand command);
    Task<Result<string>> ConfirmEmailAsync(ConfirmEmailCommand command);
    Task<Bases.Result<GoogleUserProfile?>> GoogleLoginAsync(GoogleLoginCommand command);
    Task<Result<SignInResponseDto>> LoginAsync(LoginCommand command);
    Task<Bases.Result<SignInResponseDto>> RefreshTokenAsync(RefreshTokenCommand command);
    Task<Bases.Result<bool>> RevokeTokenAsync(RevokeTokenCommand command);
    Task<Result<string>> ForgotPasswordAsync(ForgotPasswordCommand command);
    Task<Result<string>> ConfirmForgotPasswordCodeAsync(ConfirmForgotPasswordCodeCommand command);
    Task<Result<string>> Enable2FaAsync(Enable2FaCommand command);
    Task<Result<string>> ConfirmEnable2FaAsync(ConfirmEnable2FaCommand command);
    Task<Result<SignInResponseDto>> Verify2FaCodeAsync(Verify2FaCodeCommand command);
    Task<Result<string>> Disable2FaAsync(Disable2FaCommand command);
    Task<Bases.Result<ValidateTokenResponseDto>> ValidateTokenAsync(ValidateTokenCommand command);
    Task LogoutAsync();
}