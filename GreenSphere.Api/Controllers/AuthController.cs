using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace GreenSphere.Api.Controllers;
[AllowAnonymous]
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/auth")]
[ApiController]
public class AuthController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(SuccessResult<SignUpResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<SignUpResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<SignUpResponseDto>>> RegisterUserAsync(RegisterCommand command)
        => CustomResult(await _mediator.Send(command));

    [HttpPost("sendConfirmEmailCode")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(SuccessResult<SendCodeConfirmEmailResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<SendCodeConfirmEmailResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<SendCodeConfirmEmailResponseDto>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<string>>> SendConfirmEmailCodeAsync(SendConfirmEmailCodeCommand command)
        => CustomResult(await _mediator.Send(command));

    [HttpPost("confirm-email")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> ConfirmEmailAsync(ConfirmEmailCommand command)
       => CustomResult(await _mediator.Send(command));

    [HttpPost("signOut")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> SignOutAsync()
    {
        await _mediator.Send(new LogoutCommand());
        return Ok();
    }

    [HttpPost("login-google")]
    public async Task<ActionResult<Result<GoogleAuthResponseDto>>> GoogleLoginAsync(GoogleLoginCommand command)
        => CustomResult(await _mediator.Send(command));

    [HttpPost("login-facebook")]
    public async Task<ActionResult<Result<string>>> FacebookLoginAsync(FacebookLoginCommand command)
        => CustomResult(await _mediator.Send(command));

    [HttpPost("login-user")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(SuccessResult<SignInResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<SignInResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<SignInResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<SignInResponseDto>>> LoginUserAsync(LoginCommand command)
        => CustomResult(await _mediator.Send(command));

    [HttpGet("refresh-token")]
    [ProducesResponseType(typeof(Application.Bases.Result<SignInResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Application.Bases.Result<SignInResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Application.Bases.Result<SignInResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Application.Bases.Result<SignInResponseDto>>> RefreshTokenAsync()
    {
        var refreshToken = Encoding.UTF8.GetString(Convert.FromBase64String(Request.Cookies["refreshToken"]!));
        var authResponseResult = await _mediator.Send(new RefreshTokenCommand { Token = refreshToken! });
        if (authResponseResult.Value is not null && authResponseResult.Value.IsAuthenticated)
            SetRefreshTokenInCookie(Convert.ToBase64String(Encoding.UTF8.GetBytes(authResponseResult.Value.RefreshToken!)), authResponseResult.Value.RefreshTokenExpiration);

        return CustomResult(authResponseResult);
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeTokenAsync(RevokeTokenCommand command)
    {
        if (command.Token is not null) return CustomResult(await _mediator.Send(command));
        command = new RevokeTokenCommand { Token = Encoding.UTF8.GetString(Convert.FromBase64String(Request.Cookies["refreshToken"]!)) };
        return CustomResult(await _mediator.Send(command));
    }

    private void SetRefreshTokenInCookie(string valueRefreshToken, DateTimeOffset valueRefreshTokenExpiration)
    {
        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            Expires = valueRefreshTokenExpiration,
        };

        Response.Cookies.Append("refreshToken", valueRefreshToken, cookieOptions);
    }


    [HttpPost("forgot-password")]
    public async Task<ActionResult<Result<string>>> ForgotPasswordAsync(ForgotPasswordCommand command)
    {
        return CustomResult(await _mediator.Send(command));
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<Result<string>>> ConfirmForgotPasswordCodeAsync(ConfirmForgotPasswordCodeCommand command)
    {
        return CustomResult(await _mediator.Send(command));
    }


    
}
