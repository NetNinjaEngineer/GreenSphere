using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Handlers.Commands;
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
    Application.Bases.Result<SignInResponseDto> RefreshToken(RefreshTokenCommand command);
    Application.Bases.Result<bool> RevokeTokenAsync(RevokeTokenCommand command);



    Task<Result<string>> ForgotPasswordAsync(ForgotPasswordCommand command);

    Task<Result<string>> ConfirmForgotPasswordCodeAsync(ConfirmForgotPasswordCodeCommand command);
  



}
