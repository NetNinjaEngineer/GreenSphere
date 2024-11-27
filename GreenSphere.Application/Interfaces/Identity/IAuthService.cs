using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;

namespace GreenSphere.Application.Interfaces.Identity;

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