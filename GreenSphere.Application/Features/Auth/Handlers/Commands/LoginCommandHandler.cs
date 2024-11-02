using Azure;
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;

public sealed class LoginCommandHandler(
    IAuthService authService,
    IHttpContextAccessor contextAccessor) : IRequestHandler<LoginCommand, Result<SignInResponseDto>>
{
    public async Task<Result<SignInResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginAsync(request);
        if (response is not SuccessResult<SignInResponseDto> loginResponse) return response;
        if (!string.IsNullOrEmpty(loginResponse.Data.RefreshToken))
            SetRefreshTokenInCookie(loginResponse.Data.RefreshToken, loginResponse.Data.RefreshTokenExpiration);
        return response;
    }
    
    private void SetRefreshTokenInCookie(string refreshToken, DateTimeOffset expiresOn)
    {
        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            Expires = expiresOn,
        };
        
        contextAccessor.HttpContext!.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}