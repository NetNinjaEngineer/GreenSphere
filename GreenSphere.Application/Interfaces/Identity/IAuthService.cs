using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;

namespace GreenSphere.Application.Interfaces.Identity;
public interface IAuthService
{
    Task<Result<SignUpResponseDto>> RegisterAsync(RegisterCommand command);
    Task<Result<SendCodeConfirmEmailResponseDto>> SendConfirmEmailCodeAsync(string email);
    Task<Result<string>> ConfirmEmailAsync(string email, string code);
    Task LogoutAsync();
}
