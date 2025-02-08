using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Users.Commands.ChangeUserEmail;
using GreenSphere.Application.Features.Users.Commands.ChangeUserPassword;
using GreenSphere.Application.Features.Users.Commands.EditUserProfile;
using GreenSphere.Application.Features.Users.Commands.VerifyChangeUserEmail;
using GreenSphere.Application.Features.Users.Queries.GetUserProfile;
using GreenSphere.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[Guard]
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/users")]
[ApiController]
public class UsersController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet("profile")]
    [Guard(roles: [Constants.Roles.User])]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<UserProfileDto>>> GetUserProfileAsync()
        => CustomResult(await Mediator.Send(new GetUserProfileQuery()));

    [Guard]
    [HttpPost("edit-profile")]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<UserProfileDto>>> EditUserProfileAsync(
           [FromForm] EditUserProfileCommand request)
    {

        var result = await Mediator.Send(request);

        return CustomResult(result);
    }
    [Guard]
    [HttpPost("change-email")]
    [ProducesResponseType(typeof(Result<ChangeUserEmailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ChangeUserEmailDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<ChangeUserEmailDto>>> ChangeUserEmailAsync(
    [FromBody] ChangeUserEmailCommand request)
    {
        var result = await Mediator.Send(request);
        return CustomResult(result);
    }

    [Guard]
    [HttpPost("verify-change-email")]
    [ProducesResponseType(typeof(Result<VerifyChangeUserEmailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<VerifyChangeUserEmailDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<VerifyChangeUserEmailDto>>> VerifyChangeUserEmailAsync(
    [FromBody] VerifyChangeUserEmailCommand request)
    {
        var result = await Mediator.Send(request);
        return CustomResult(result);
    }

    [Guard]
    [HttpPost("change-password")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<bool>>> ChangePasswordAsync(
    [FromBody] ChangeUserPasswordCommand request)
    {
        var result = await Mediator.Send(request);
        return CustomResult(result);
    }

}