using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.DTOs.Auth;
using GreenSphere.Application.Features.Auth.Commands.ConfirmEmail;
using GreenSphere.Application.Features.Auth.Commands.ConfirmEnable2FA;
using GreenSphere.Application.Features.Auth.Commands.ConfirmForgotPasswordCode;
using GreenSphere.Application.Features.Auth.Commands.Disable2Fa;
using GreenSphere.Application.Features.Auth.Commands.Enable2Fa;
using GreenSphere.Application.Features.Auth.Commands.FacebookLogin;
using GreenSphere.Application.Features.Auth.Commands.ForgotPassword;
using GreenSphere.Application.Features.Auth.Commands.GoogleLogin;
using GreenSphere.Application.Features.Auth.Commands.Login;
using GreenSphere.Application.Features.Auth.Commands.Logout;
using GreenSphere.Application.Features.Auth.Commands.RefreshToken;
using GreenSphere.Application.Features.Auth.Commands.Register;
using GreenSphere.Application.Features.Auth.Commands.RevokeToken;
using GreenSphere.Application.Features.Auth.Commands.SendConfirmEmailCode;
using GreenSphere.Application.Features.Auth.Commands.ValidateToken;
using GreenSphere.Application.Features.Auth.Commands.Verify2FaCode;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace GreenSphere.Api.Controllers;

[AllowAnonymous]
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/auth")]
public class AuthController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(SuccessResult<SignUpResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<SignUpResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<SignUpResponseDto>>> RegisterUserAsync(RegisterCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("sendConfirmEmailCode")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(SuccessResult<SendCodeConfirmEmailResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<SendCodeConfirmEmailResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<SendCodeConfirmEmailResponseDto>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<string>>> SendConfirmEmailCodeAsync(SendConfirmEmailCodeCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("confirm-email")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> ConfirmEmailAsync(ConfirmEmailCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("signOut")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> SignOutAsync()
    {
        await Mediator.Send(new LogoutCommand());
        return Ok();
    }

    [HttpPost("login-google")]
    public async Task<ActionResult<Application.Bases.Result<GoogleUserProfile?>>> GoogleLoginAsync(
        [FromQuery] GoogleLoginCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("login-facebook")]
    public async Task<ActionResult<Result<string>>> FacebookLoginAsync(FacebookLoginCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("login-user")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(SuccessResult<SignInResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<SignInResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FailedResult<SignInResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<SignInResponseDto>>> LoginUserAsync(LoginCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpGet("refresh-token")]
    [ProducesResponseType(typeof(Application.Bases.Result<SignInResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Application.Bases.Result<SignInResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Application.Bases.Result<SignInResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Application.Bases.Result<SignInResponseDto>>> RefreshTokenAsync()
    {
        var refreshToken = Encoding.UTF8.GetString(Convert.FromBase64String(Request.Cookies["refreshToken"]!));
        var authResponseResult = await Mediator.Send(new RefreshTokenCommand { Token = refreshToken! });
        if (authResponseResult.Value is not null && authResponseResult.Value.IsAuthenticated)
            SetRefreshTokenInCookie(
                Convert.ToBase64String(Encoding.UTF8.GetBytes(authResponseResult.Value.RefreshToken!)),
                authResponseResult.Value.RefreshTokenExpiration);

        return CustomResult(authResponseResult);
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeTokenAsync(RevokeTokenCommand command)
    {
        if (command.Token is not null) return CustomResult(await Mediator.Send(command));
        command = new RevokeTokenCommand
        { Token = Encoding.UTF8.GetString(Convert.FromBase64String(Request.Cookies["refreshToken"]!)) };
        return CustomResult(await Mediator.Send(command));
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
        return CustomResult(await Mediator.Send(command));
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<Result<string>>> ConfirmForgotPasswordCodeAsync(
        ConfirmForgotPasswordCodeCommand command)
    {
        return CustomResult(await Mediator.Send(command));
    }


    [HttpPost("enable-2fa")]
    public async Task<ActionResult<Result<string>>> Enable2FaAsync(Enable2FaCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("confirm-enable-2fa")]
    public async Task<ActionResult<Result<string>>> ConfirmEnable2FaAsync(ConfirmEnable2FaCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("verify-2fa")]
    public async Task<ActionResult<Result<string>>> Verify2FaAsync(Verify2FaCodeCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("disable-2fa")]
    public async Task<ActionResult<Result<string>>> Disable2FaAsync(Disable2FaCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("validate-token")]
    public async Task<ActionResult<Application.Bases.Result<ValidateTokenResponseDto>>> ValidateTokenAsync([FromQuery] ValidateTokenCommand command)
        => CustomResult(await Mediator.Send(command));
}