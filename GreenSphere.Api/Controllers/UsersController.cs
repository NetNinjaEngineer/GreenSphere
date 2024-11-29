using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Users.DTOs;
using GreenSphere.Application.Features.Users.Requests.Commands;
using GreenSphere.Application.Features.Users.Requests.Queries;
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
    [Guard(roles: [Constants.Roles.User])]
    [HttpPost("{userId}/privacy-settings/assign")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<string>>> AssignPrivacySettingAsync(string userId,
        AssignPrivacySettingsRequestDto privacySettingRequest)
        => CustomResult(await Mediator.Send(new AssignUserPrivacyCommand
        { UserId = userId, AssignPrivacySettingsRequest = privacySettingRequest }));

    [Guard(roles: [Constants.Roles.User, Constants.Roles.Admin])]
    [HttpGet("{userId}/privacy-settings")]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<PrivacySettingListDto>>> GetUserPrivacySettingsAsync(string userId)
        => CustomResult(await Mediator.Send(new GetUserPrivacySettingQuery { UserId = userId }));

    [Guard(policies: [Constants.User.CanViewProfilePolicy])]
    [HttpGet("{userId}/profile")]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<PrivacySettingListDto>>> GetUserProfileAsync(string userId)
        => CustomResult(await Mediator.Send(new GetUserProfileQuery { UserId = userId }));

    [Guard(roles: [Constants.Roles.User])]
    [HttpGet("loggedInUser/profile")]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserProfileDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<UserProfileDto>>> GetUserProfileAsync()
        => CustomResult(await Mediator.Send(new GetLoggedInUserProfileQuery()));

    [Guard(roles: [Constants.Roles.User])]
    [HttpGet("loggedInUser/privacy-settings")]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<PrivacySettingListDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<PrivacySettingListDto>>> GetUserPrivacySettingsAsync()
        => CustomResult(await Mediator.Send(new GetLoggedInUserPrivacySettingsQuery()));
}